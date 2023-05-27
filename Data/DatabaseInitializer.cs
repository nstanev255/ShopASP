using Microsoft.EntityFrameworkCore;
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
        var genreEntities = applicationDbContext.Genres;
        Console.WriteLine("Any " + genreEntities.Any());
        if (genreModels != null && !genreEntities.Any())
        { 
            // Add genres to the database
            foreach (var genre in genreModels)
            {
                Genre genreEntity = new Genre() { Id = genre.Id, Name = genre.Name };
                genreEntities.Add(genreEntity);
            }

            // Add genres to the database.
            await applicationDbContext.Genres.AddRangeAsync(genreEntities);
            await applicationDbContext.SaveChangesAsync();
        }
        Console.WriteLine("outside cateogies");
        
        var dbCategories = applicationDbContext.Categories;
        // Fill up categories OR Platforms for the api.
        if (!dbCategories.Any())
        {
            Console.WriteLine("here");
            List<Category> categories = new List<Category>();

            // Add PC
            var pcCategory = new Category { Id = 6, Type = CategoryType.PC };
            categories.Add(pcCategory);

            // Add Playstation 4
            var ps4Category = new Category { Id = 48, Type = CategoryType.PLAYSTATION_4 };
            categories.Add(ps4Category);

            var ps5Category = new Category { Id = 167, Type = CategoryType.PLAYSTATION_5 };
            categories.Add(ps5Category);

            var xboxXsCategory = new Category { Id = 169, Type = CategoryType.XBOX_XS };
            categories.Add(xboxXsCategory);

            var xboxOneCategory = new Category { Id = 49, Type = CategoryType.XBOX_ONE };
            categories.Add(xboxOneCategory);

            var nintendoSwCategory = new Category { Id = 130, Type = CategoryType.NINTENDO_SW };
            categories.Add(nintendoSwCategory);

            // Save to the database.
            await applicationDbContext.Categories.AddRangeAsync(categories);
            await applicationDbContext.SaveChangesAsync();
        }

        Console.WriteLine("here");
        // Fill up the games, with the needed images, platforms and developers.
        DbSet<Product> products = applicationDbContext.Products;
        List<GameModel>? games = await api.Games(0);
        if (games != null && !products.Any())
        {
            foreach (var game in games)
            {
                Console.WriteLine("Game " + game);
            }
        }
    }
}