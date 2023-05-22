using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopASP.Models.Entity;
using ShopASP.Services;

namespace ShopASP.Controllers;

public class ProductController : Controller
{
    private ILogger<ProductController> _logger;
    private IProductService _productService;
    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }
    
    [AllowAnonymous]
    [HttpGet("{category}")]
    public async Task<IActionResult> ProductList(string category)
    {
        CategoryType parsedCategory;
        if (Enum.TryParse(category.ToUpper(), out parsedCategory))
        {
            _productService.FindAllByCategory(parsedCategory);
        }
        else
        {
            // Return 404 if we can't parse the enum.
            return NotFound();
        }

        return View();
    }
}