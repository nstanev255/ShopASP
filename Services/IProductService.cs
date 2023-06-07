using ShopASP.Models.Entity;

namespace ShopASP.Services;

public interface IProductService
{
    public List<Product> FindAllByCategories(List<CategoryType> categoryTypes, int page);
    public Task<int> CountProductsByCategories(List<CategoryType> categoryTypes);
}