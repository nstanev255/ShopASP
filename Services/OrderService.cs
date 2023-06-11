using Microsoft.EntityFrameworkCore;
using ShopASP.Models;
using ShopASP.Models.Entity;
using ShopASP.Areas.Identity.Services;
using ShopASP.Data;
using ShopASP.Models.Mail;
using ShopASP.Utils;

namespace ShopASP.Services;

public class OrderService : IOrderService
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IMailService _mailService;
    public IConfigurationService _configurationService { get; set; }

    private readonly DbSet<Order> _dao;
    private readonly ApplicationDbContext _dbContext;

    public OrderService(IProductService productService, ICategoryService categoryService,
        IAuthenticationService authenticationService, IMailService mailService,
        IConfigurationService configuration, ApplicationDbContext context)
    {
        _productService = productService;
        _categoryService = categoryService;
        _authenticationService = authenticationService;
        _mailService = mailService;
        _configurationService = configuration;

        _dbContext = context;
        _dao = context.Orders;
    }

    private async Task<Order?> FindByIdAsync(int id)
    {
        return await _dao.FirstOrDefaultAsync(o => o.Id == id);
    }

    private async Task<Order?> FindByUUIDAsync(string uuid)
    {
        return await _dao.FirstOrDefaultAsync(o => o.UUID == uuid);
    }

    private async Task<Order?> FindByUUIDAsyncFetch(string uuid)
    {
        return await _dao.Where(o => o.UUID == uuid)
            .Include(o => o.User)
            .Include(o => o.Products).ThenInclude(p => p.Product)
            .FirstOrDefaultAsync();
    }

    public async Task AcceptOrder(string orderId)
    {
        var order = await FindByUUIDAsyncFetch(orderId);
        if (order == null)
        {
            throw new Exception("Could not find order");
        }

        if (order.Status != OrderStatus.NOT_PROCESSED)
        {
            throw new Exception("Order already processed");
        }

        AcceptOrderDb(order);
        await SendConfirmationOrderEmail(order, true);

        await _dbContext.SaveChangesAsync();
    }

    private List<Product> OrderProductToProduct(List<OrderProduct> orderProducts)
    {
        Console.WriteLine("OrderProducts" + orderProducts);
        var products = new List<Product>();
        if (orderProducts.Count == 0)
        {
            return products;
        }

        foreach (var orderProduct in orderProducts)
        {
            var product = orderProduct.Product;

            Console.WriteLine("Product " + product);
            products.Add(product);
        }

        return products;
    }

    public async Task RejectOrder(string orderId)
    {
        var order = await FindByUUIDAsyncFetch(orderId);
        if (order == null)
        {
            throw new Exception("Order does not exist");
        }

        if (order.Status != OrderStatus.NOT_PROCESSED)
        {
            throw new Exception("Order is already processed");
        }

        RejectOrderDb(order);
        _productService.AddToQuantityMany(OrderProductToProduct(order.Products), 1);
        await SendConfirmationOrderEmail(order, false);

        await _dbContext.SaveChangesAsync();
    }

    private void RejectOrderDb(Order order)
    {
        order.Status = OrderStatus.REJECTED;
        _dao.Update(order);
    }

    private async Task SendConfirmationOrderEmail(Order order, bool accepted)
    {
        var mail = new Mail
        {
            Subject = "Accepted Order !",
            Body = $"The order {order.UUID} has been {(accepted ? "accepted" : "rejected")} !",
            Recepient = order.User.UserName
        };
        await _mailService.SendEmail(mail);
    }

    private void AcceptOrderDb(Order order)
    {
        order.Status = OrderStatus.ACCEPTED;
        _dao.Update(order);
    }

    public async Task PlaceSingleOrder(SingleOrderInputModel input)
    {
        var product = await _productService.FindByIdAsync(input.ProductId);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        var category = _categoryService.FindOneById(input.CategoryId);
        if (category == null)
        {
            throw new Exception("Category not found");
        }

        var order = await FindByUUIDAsync(input.OrderId);
        if (order != null)
        {
            throw new Exception("Order already exists");
        }

        // Remove from the quantity of the product.
        _productService.RemoveFromQuantity(product, 1);

        // Create the order.
        var orderProduct = new OrderProduct { Product = product, Category = category, Price = product.Price };
        var orderId = await CreateOrder(new List<OrderProduct> { orderProduct }, input.OrderId);
        // If we are here, then this means that we can send the emails.

        // This will send to the orderer that their order is placed successfully.
        await SendOrderEmail(orderId);

        // This will send an email to the Shop, where they can decide if they want to accept the order.
        await SendRejectAcceptEmail(orderId);

        // After all is done, we will save the changes.
        await _dbContext.SaveChangesAsync();
    }

    private async Task SendOrderEmail(string orderId)
    {
        var mail = new Mail();
        mail.Body = "Successfully placed order " + orderId;
        mail.Recepient = _authenticationService.GetCurrentUser().Name;
        mail.Subject = "Successful Order !";

        await _mailService.SendEmail(mail);
    }

    /**
     * This will send an email to the shop's owner, stating that there is a new order.
     * The shop owner will decide if they want to accept or reject the order, through links.
     */
    private async Task SendRejectAcceptEmail(string orderId)
    {
        var acceptLink = $"<a href='{Constants.Constants.DOMAIN}/order/accept/{orderId}'> Accept Order </a>";
        var rejectLink = $"<a href='{Constants.Constants.DOMAIN}/order/reject/{orderId}'> Reject Order </a>";

        var mailBody = $"<div> {acceptLink} | {rejectLink} </div>";

        var mail = new Mail
            { Subject = "New Order !", Body = mailBody, Recepient = _configurationService.GetShopEmail() };

        await _mailService.SendEmail(mail);
    }

    private async Task<string> CreateOrder(List<OrderProduct> orderProducts, string uuid)
    {
        var identity = _authenticationService.GetCurrentUser();
        if (identity.Name == null)
        {
            throw new Exception("Username is not available");
        }

        var user = _authenticationService.FindUserByUsername(identity.Name);
        if (user == null)
        {
            throw new Exception("User does not exist");
        }

        List<decimal> prices = Utils.PriceUtils.OrderProductPrices(orderProducts);

        Order order = new Order();
        order.Products = orderProducts;
        order.User = user;
        order.FinalPrice = Utils.PriceUtils.CalculateFinalPrice(prices);
        order.Status = OrderStatus.NOT_PROCESSED;
        order.UUID = uuid;

        var record = await _dao.AddAsync(order);

        return record.Entity.UUID;
    }

    public int CountAll()
    {
        return _dao.Count();
    }

    public List<Order> FindAllPaginate(int page)
    {
        var offset = PaginationUtils.CalculateOffset(page);
        return _dao.Include(o => o.Products)
            .ThenInclude(p => p.Product).Skip(offset).Take(Constants.Constants.ItemsPerPage).ToList();
    }
}