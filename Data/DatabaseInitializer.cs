using ShopASP.Api;
using ShopASP.Api.Model;
using ShopASP.Models.Entity;

namespace ShopASP.Data;

public class DatabaseInitializer
{
    public static async Task SeedData(ApplicationDbContext applicationDbContext)
    {
        var api = new GdbApi();
        List<GenreModel>? genreModels = await api.GenreModels();
        var genreEntities = applicationDbContext.Genres.ToList();
        if (genreModels != null && genreEntities.Count == 0)
        {
            // Add genres to the databse
            foreach (var genre in genreModels)
            {
                Genre genreEntity = new Genre() { Id = genre.Id, Name = genre.Name};
                genreEntities.Add(genreEntity);
            }
        }

        // Add genres to the database.
        await applicationDbContext.Genres.AddRangeAsync(genreEntities);
        await applicationDbContext.SaveChangesAsync();


    }
}