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
        var genreEntities = applicationDbContext.Genres;
        List<GenreModel>? genreModels = await api.GenreModels();
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
                
                // Check if the most important fields are here,
                // We don't need any game which doesn't have these values
                if (game.Name == null || game.Summary == null)
                {
                    continue;
                }

                var product = products.FirstOrDefault(p => p.Id == game.Id);
                if (product != null)
                {
                    continue;
                }

                product = new Product();

                List<InvolvedCompanyModel>? involvedCompanies = await api.InvolvedCompaniesByGameID(game.Id);

                var developer = await HandleDeveloper(involvedCompanies, developerService, api,
                    applicationDbContext.Developers, applicationDbContext);
                if (developer == null)
                {
                    // If there are no developers, we don't need the game for now.
                    continue;
                }
                var cover = await HandleCover(game.Cover, api, applicationDbContext.Images);

                var genres = await HandleGenres(game.Genres, genreService, product);
                var categories = HandleCategories(game.Platforms, product, categoryService);
                // If the game does not have categories, we don't need it.
                if (categories.Count == 0)
                {
                    continue;
                }


                product.Id = game.Id;
                product.Name = game.Name;
                product.Description = game.Summary;
                product.ReleaseDate = DateOnly.FromDateTime(UnixTimeStampToDateTime(game.FirstReleaseDate));
                product.Categories = categories;
                product.Developer = developer;
                product.Genres = genres;
                product.Price = 59.99M;
                product.Units = 0;
                product.FrontCover = cover;
                
                await applicationDbContext.Products.AddAsync(product);
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }

    private static async Task<Image?> HandleCover(int gameCover, GdbApi api, DbSet<Image> dao)
    {
        var cover = await api.Cover(gameCover);
        Image? image = null;
        if (cover != null)
        {
            image = await dao.FirstOrDefaultAsync(i => i.Id == cover.Id);
            if (image == null)
            {
                image = new Image();
                image.Id = cover.Id;
                image.Name = cover.ImageId;
                // Make the URL to the image to get the big cover.
                image.Url = cover.Url.Replace("t_thumb", "t_cover_big");

                await dao.AddAsync(image);
            }
        }

        return image;
    }

    private static async Task<ICollection<ProductGenre>?> HandleGenres(List<int>? gameGenres,
        IGenreService genreService, Product product)
    {
        var productGenres = new List<ProductGenre>();

        foreach (var genreId in gameGenres)
        {
            var dbGenre = await genreService.FindOneById(genreId);

            if (dbGenre != null)
            {
                var relationship = new ProductGenre();
                relationship.Genre = dbGenre;
                relationship.Product = product;

                productGenres.Add(relationship);
            }
        }

        return productGenres;
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

    private static async Task<Developer?> HandleDeveloper(List<InvolvedCompanyModel>? involvedCompanies,
        IDeveloperService developerService, GdbApi api, DbSet<Developer> dao, ApplicationDbContext dbContext)
    {
        Developer? developer = null;
        if (involvedCompanies == null || !involvedCompanies.Any())
        {
            return developer;
        }

        foreach (var company in involvedCompanies)
        {
            // If the company is developer
            if (company.developer)
            {
                // We check if the said developer already exists
                developer = developerService.FindOneById(company.company);
                // If the developer does not exists, we will just create it here.
                Console.WriteLine(developer);
                if (developer == null)
                {
                    // We get the actual company info.
                    var companyInfo = await api.CompanyInfo(company.company);
                    if (companyInfo != null)
                    {
                        developer = new Developer { Id = companyInfo.Id, Name = companyInfo.Name };
                        await dao.AddAsync(developer);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
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