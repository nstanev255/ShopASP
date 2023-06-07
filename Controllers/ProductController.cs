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
        ViewData["category"] = category;
        var allGenres = _genreService.FindAll();

        List<CategoryType> categoryTypes = new List<CategoryType>();


        if (category == Constants.Constants.GENERIC_PC_CATEGORY)
        {
            categoryTypes.Add(CategoryType.PC);
        }
        else if (category == Constants.Constants.GENERIC_PLAYSTATION_CATEGORY)
        {
            // If we are in the playstation page, we will return all of the playstation games (for 4 and 5)
            categoryTypes.Add(CategoryType.PLAYSTATION_4);
            categoryTypes.Add(CategoryType.PLAYSTATION_5);
        }
        else if (category == Constants.Constants.GENERIC_XBOX_CATEGORY)
        {
            // If we are in xbox page, we will return all of the xbox games.
            categoryTypes.Add(CategoryType.XBOX_ONE);
            categoryTypes.Add(CategoryType.XBOX_XS);
        }
        else if (category == Constants.Constants.GENERIC_NINTENDO_CATEGORY)
        {
            categoryTypes.Add(CategoryType.NINTENDO_SW);
        }
        else
        {
            // If the category is not handled, we will return a 404 page.
            return NotFound();
        }

        List<Product> products = _productService.FindAllByCategories(categoryTypes, page);
        int allProducts = await _productService.CountProductsByCategories(categoryTypes);

        int allPages = (int)Math.Round(allProducts / Constants.Constants.ItemsPerPage + 0.0M,
            MidpointRounding.AwayFromZero);
        var model = new ProductListViewModel { Products = products, Genres = allGenres, AllPages = allPages };
        
        return View(model: model);
    }

    [AllowAnonymous]
    [HttpGet("/product/{productId}")]
    public async Task<IActionResult> Product(int productId)
    {
        _productService.FindByIdAsync(productId);
        return View();
    }
}