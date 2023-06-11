using Microsoft.EntityFrameworkCore;
using ShopASP.Models;
using ShopASP.Models.Entity;
using ShopASP.Areas.Identity.Services;
using ShopASP.Data;

namespace ShopASP.Services;

public class OrderService : IOrderService
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IAuthenticationService _authenticationService;

    private readonly DbSet<Order> _dao;
    private readonly ApplicationDbContext _dbContext;

    public OrderService(IProductService productService, ICategoryService categoryService, 
        IAuthenticationService authenticationService, ApplicationDbContext context)
    {
        _productService = productService;
        _categoryService = categoryService;
        _authenticationService = authenticationService;
        
        _dbContext = context;
        _dao = context.Orders;
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

        // Create the order.
        var orderProduct = new OrderProduct { Product = product, Category = category, Price = product.Price };
        var orderId = await CreateOrder(new List<OrderProduct> { orderProduct });
    }

    private async Task<int> CreateOrder(List<OrderProduct> orderProducts)
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

        var record = await _dao.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return record.Entity.Id;
    }
}