using Microsoft.EntityFrameworkCore;
using ShopASP.Api;
using ShopASP.Api.Model;
using ShopASP.Models.Entity;
using ShopASP.Services;

namespace ShopASP.Data;

public class DatabaseInitializer
{
    public static async Task SeedData(ApplicationDbContext applicationDbContext,
        IGenreService genreService, ICategoryService categoryService, IDeveloperService developerService)
    {
        var api = new GdbApi();
        List<GenreModel>? genreModels = await api.GenreModels();
        var genreEntities = applicationDbContext.Genres;
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

        var dbCategories = applicationDbContext.Categories;
        // Fill up categories OR Platforms for the api.
        if (!dbCategories.Any())
        {
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

        // Fill up the games, with the needed images, platforms and developers.
        DbSet<Product> products = applicationDbContext.Products;
        List<GameModel>? games = await api.Games(0);
        if (games != null && !products.Any())
        {
            foreach (var game in games)
            {
                Product product = new Product();

                product.Name = game.Name;
                product.Description = game.Summary;
                product.ReleaseDate = DateOnly.FromDateTime(UnixTimeStampToDateTime(game.FirstReleaseDate));
                product.Categories = HandleCategories(game.Platforms, product, categoryService);
                product.Developer = HandleDeveloper(game.InvolvedCompanies);

                await applicationDbContext.AddAsync(product);
            }
        }
    }

    private static List<CategoryProduct> HandleCategories(List<int>? categoryIds, Product product,
        ICategoryService categoryService)
    {
        var relationships = new List<CategoryProduct>();
        if (categoryIds == null || !categoryIds.Any())
        {
            return relationships;
        }

        foreach (var id in categoryIds)
        {
            var category = categoryService.FindOneById(id);
            if (category != null)
            {
                var relationship = new CategoryProduct { Product = product, Category = category };
                relationships.Add(relationship);
            }
        }

        return relationships;
    }

    private static Developer HandleDeveloper(List<int>? gameInvolvedCompanies)
    {
        Developer developer = new Developer();
        if (gameInvolvedCompanies == null || !gameInvolvedCompanies.Any())
        {
            return developer;
        }

        foreach (var company in gameInvolvedCompanies)
        {
        }

        return developer;
    }

    public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }
}