using Microsoft.EntityFrameworkCore;
using ShopASP.Data;
using ShopASP.Models.Entity;
using ShopASP.Utils;

namespace ShopASP.Services;

public class ProductService : IProductService
{
    private readonly DbSet<Product> _productDao;

    public ProductService(ApplicationDbContext context)
    {
        _productDao = context.Products;
    }

    public List<Product> FindAllByCategories(List<CategoryType> categoryTypes, int page)
    {
        int offset = PaginationUtils.CalculateOffset(page);

        return _productDao
            .Where(p => p.Categories.Any(cp => categoryTypes.Contains(cp.Category.Type)))
            .Include(p => (p as Product).FrontCover)
            .Skip(offset)
            .Take(Constants.Constants.ItemsPerPage)
            .ToList();
    }

    public async Task<int> CountProductsByCategories(List<CategoryType> categoryTypes)
    {
        return await _productDao.CountAsync(p => p.Categories.Any(cp => categoryTypes.Contains(cp.Category.Type)));
    }

    public async Task<Product?> FindByIdAsync(int productId)
    {
        return await _productDao.Where(p => p.Id == productId)
            .Include(p => p.Genres).ThenInclude(g => g.Genre)
            .Include(p => p.Categories).ThenInclude(c => c.Category)
            .Include(p => p.Developer)
            .Include(p => p.FrontCover)
            .Include(p => p.MinimumSystemRequirements)
            .Include(p => p.RecommendedSystemRequirements)
            .Include(p => p.Screenshots)
            .FirstOrDefaultAsync();
    }

    public void RemoveFromQuantity(Product product, uint toRemove)
    {
        if (product.Units <= 0)
        {
            throw new Exception("The product is already unavailable");
        }

        product.Units -= (int)toRemove;
        _productDao.Update(product);
    }

    public void AddToQuantityMany(List<Product> products, uint toAdd)
    {
        if (products.Count == 0)
        {
            return;
        }

        foreach (var product in products)
        {
            AddToQuantity(product, toAdd);
        }
    }

    public void AddToQuantity(Product product, uint toAdd)
    {
        product.Units += (int) toAdd;
        _productDao.Update(product);
    }
}