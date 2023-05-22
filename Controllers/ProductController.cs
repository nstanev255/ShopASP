using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopASP.Models.Entity;

namespace ShopASP.Controllers;

public class ProductController : Controller
{
    private ILogger<ProductController> _logger;
    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }
    
    [AllowAnonymous]
    [HttpGet("{category}")]
    public async Task<IActionResult> ProductList(string category)
    {
        return View();
    }
}