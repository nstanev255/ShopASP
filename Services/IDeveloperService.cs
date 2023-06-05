using ShopASP.Models.Entity;

namespace ShopASP.Services;

public interface IDeveloperService
{
    public Developer? FindOneById(int id);
}