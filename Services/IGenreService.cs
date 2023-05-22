using ShopASP.Models.Entity;

namespace ShopASP.Services;

public interface IGenreService
{
    List<Genre> FindAll();
}