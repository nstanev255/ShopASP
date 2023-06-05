using Microsoft.EntityFrameworkCore;
using ShopASP.Data;
using ShopASP.Models.Entity;

namespace ShopASP.Services;

public class GenreService : IGenreService
{
    private readonly DbSet<Genre> _genresDao;
    public GenreService(ApplicationDbContext dbContext)
    {
        _genresDao = dbContext.Genres; 
    }
    
    public List<Genre> FindAll()
    {
        return _genresDao.ToList();
    }

    public async Task<Genre?> FindOneById(int id)
    {
        return await _genresDao.FirstAsync();
    }
}