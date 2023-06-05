using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Models;
using ShopASP.Models.Entity;
using ShopASP.Services;

namespace ShopASP.Controllers;

public class ProductController : Controller
{
    private ILogger<ProductController> _logger;
    private IProductService _productService;
    private IGenreService _genreService;

    public ProductController(ILogger<ProductController> logger, IProductService productService,
        IGenreService genreService)
    {
        _logger = logger;
        _productService = productService;
        _genreService = genreService;
    }

    [AllowAnonymous]
    [HttpGet("{category}")]
    public async Task<IActionResult> ProductList(string category)
    {
        CategoryType parsedCategory;

        if (Enum.TryParse(category.ToUpper(), out parsedCategory))
        {
            var products = _productService.FindAllByCategory(parsedCategory);
            var allGenres = _genreService.FindAll();

            var model = new ProductListViewModel { Products = products, Genres = allGenres};
            return View(model: model);
        }

        // Return 404 if we can't parse the enum.
        return NotFound();
    }
}