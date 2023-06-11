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
    public OrderController(ILogger<OrderController> logger, IProductService productService, ICategoryService categoryService)
    {
        _logger = logger;
        _productService = productService;
        _categoryService = categoryService;


    }

    [HttpPost]
    [Route("single-order-create")]
    public async Task<IActionResult> CreateSingleOrder()
    {
        return NotFound();
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

        var basicProduct = new BasicProduct { Id = product.Id, Name = product.Name, Price = product.Price, Image = product.FrontCover.Url };
        var model = new SingleOrderViewModel { Product = basicProduct, CategoryType = category.Type, FinalPrice = product.Price};

        return View(model);
    }
}