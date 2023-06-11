using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Models;
using ShopASP.Models.Entity;
using ShopASP.Services;

namespace ShopASP.Controllers;

[Route("order")]
public class OrderController : Controller
{
    private ILogger<OrderController> _logger;
    private IProductService _productService;
    private ICategoryService _categoryService;
    private IOrderService _orderService;

    public OrderController(ILogger<OrderController> logger, IProductService productService,
        ICategoryService categoryService,
        IOrderService orderService)
    {
        _logger = logger;
        _productService = productService;
        _categoryService = categoryService;
        _orderService = orderService;
    }

    [HttpPost]
    [Authorize]
    [Route("single-order-create")]
    public async Task<IActionResult> CreateSingleOrder(SingleOrderInputModel inputModel)
    {
        Console.WriteLine("Here");
        try
        {
            await _orderService.PlaceSingleOrder(inputModel);
            return View("SuccessfulOrder");
        }
        catch (Exception exception)
        {
            string str = exception.ToString();
            _logger.LogInformation(str);
            //TODO: Show here a failed place order view.
            return NotFound();
        }
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("accept/{orderId:required}")]
    public async Task<IActionResult> AcceptOrder(string orderId)
    {
        try
        {
            await _orderService.AcceptOrder(orderId);
        }
        catch (Exception e)
        {
            return NotFound();
        }

        return View();
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("reject/{orderId:required}")]
    public async Task<IActionResult> RejectOrder(string orderId)
    {
        try
        {
            await _orderService.RejectOrder(orderId);
        }
        catch (Exception e)
        {
            return NotFound();
        }

        return View();
    }

    /**
     * This Action is used so that we can order a single game.
     */
    [Authorize]
    [HttpGet]
    [Route("single-order")]
    public async Task<IActionResult> SingleOrder(int productId, int categoryId)
    {
        // If the said product does not exist, we can't order it, so we will just return 404.
        var product = await _productService.FindByIdAsync(productId);
        if (product == null)
        {
            return NotFound();
        }

        // If this category does not exist, we will just return 404.
        var category = _categoryService.FindOneById(categoryId);
        if (category == null)
        {
            return NotFound();
        }

        var productPrices = Utils.PriceUtils.ProductPrices(new List<Product> { product });

        var basicProduct = new BasicProduct
            { Id = product.Id, Name = product.Name, Price = product.Price, Image = product.FrontCover.Url };
        var model = new SingleOrderViewModel
        {
            Product = basicProduct, Category = category,
            FinalPrice = Utils.PriceUtils.CalculateFinalPrice(productPrices),
            InputModel = new SingleOrderInputModel { OrderId = Utils.UUID.Generate() }
        };

        return View(model);
    }
}