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

}