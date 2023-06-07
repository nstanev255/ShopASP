using ShopASP.Models.Entity;

namespace ShopASP.Services;

public interface IProductService
{
    public List<Product> FindAllByCategory(CategoryType categoryType, int page);
    public Task<int> CountProductsByCategory(CategoryType categoryType);
}