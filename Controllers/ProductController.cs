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
    public async Task<IActionResult> ProductList(string category, int page = 1)
    {
        CategoryType parsedCategory;
        ViewData["category"] = category;

        if (Enum.TryParse(category.ToUpper(), out parsedCategory))
        {
            var products = _productService.FindAllByCategory(parsedCategory, page);
            var allGenres = _genreService.FindAll();

            int allProducts = await _productService.CountProductsByCategory(parsedCategory);
            int allPages = (int)Math.Round(allProducts / Constants.Constants.ItemsPerPage + 0.0M, MidpointRounding.AwayFromZero);

            var model = new ProductListViewModel { Products = products, Genres = allGenres, AllPages = allPages};
            return View(model: model);
        }

        // Return 404 if we can't parse the enum.
        return NotFound();
    }
}