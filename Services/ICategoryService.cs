using ShopASP.Models.Entity;

namespace ShopASP.Services;

public interface ICategoryService
{
    public Category? FindOneById(int id);
}