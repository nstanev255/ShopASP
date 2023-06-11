using ShopASP.Models.Entity;

namespace ShopASP.Services;

public interface IProductService
{
    public List<Product> FindAllByCategories(List<CategoryType> categoryTypes, int page);
    public Task<int> CountProductsByCategories(List<CategoryType> categoryTypes);
    
    /**
     * This method will retrieve a product by its id + all of the relationships.
     */
    public Task<Product?> FindByIdAsync(int productId);

    public void RemoveFromQuantity(Product product, uint toRemove);
    public void AddToQuantity(Product product, uint toAdd);

    public void AddToQuantityMany(List<Product> products, uint toAdd);
}