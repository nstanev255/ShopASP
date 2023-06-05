using ShopASP.Models.Entity;

namespace ShopASP.Services;

public interface IGenreService
{
    List<Genre> FindAll();
    public Task<Genre?> FindOneById(int id);
}