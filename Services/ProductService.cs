using Microsoft.EntityFrameworkCore;
using ShopASP.Data;
using ShopASP.Models.Entity;

namespace ShopASP.Services;

public class ProductService : IProductService
{
    private readonly DbSet<Product> _productDao;
    
    public ProductService(ApplicationDbContext context)
    {
        _productDao = context.Products;
    }

    public List<Product> FindAllByCategory(CategoryType categoryType)
    {
        return _productDao.Where(p => p.Categories.Any(cp => cp.Category.Type == categoryType)).ToList();
    }

}