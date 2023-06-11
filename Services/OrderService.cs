using Microsoft.EntityFrameworkCore;
using ShopASP.Models;
using ShopASP.Models.Entity;
using ShopASP.Areas.Identity.Services;
using ShopASP.Data;
using ShopASP.Models.Mail;

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
        return await _dao.Where(o => o.UUID == uuid).Include(o => o.User).FirstOrDefaultAsync();
    }

    public async Task AcceptOrder(string orderId)
    {
        var order = await FindByUUIDAsync(orderId);
        if (order == null)
        {
            throw new Exception("Could not find order");
        }

        if (order.Status != OrderStatus.NOT_PROCESSED)
        {
            throw new Exception("Order already processed");
        }

        await AcceptOrderDb(order);
        await SendSuccessfulOrderEmail(order);
    }

    private async Task SendSuccessfulOrderEmail(Order order)
    {
        var mail = new Mail
        {
            Subject = "Accepted Order !",
            Body = $"The order {order.UUID} has been accepted !",
            Recepient = order.User.UserName
        };
        await _mailService.SendEmail(mail);
    }

    public async Task AcceptOrderDb(Order order)
    {
        order.Status = OrderStatus.ACCEPTED;
        _dao.Update(order);
        await _dbContext.SaveChangesAsync();
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

        // Create the order.
        var orderProduct = new OrderProduct { Product = product, Category = category, Price = product.Price };
        var orderId = await CreateOrder(new List<OrderProduct> { orderProduct }, input.OrderId);
        // If we are here, then this means that we can send the emails.

        // This will send to the orderer that their order is placed successfully.
        await SendOrderEmail(orderId);

        // This will send an email to the Shop, where they can decide if they want to accept the order.
        await SendRejectAcceptEmail(orderId);
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
        await _dbContext.SaveChangesAsync();

        return record.Entity.UUID;
    }
}