using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopASP.Models.Entity;

public class Product : BaseNamed
{
    [MaxLength(3000)]
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Units { get; set; }
    public Developer? Developer { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public Image? FrontCover { get; set; }

    public List<Image>? Screenshots { get; set; }
    public List<CategoryProduct> Categories { get; set; }
    public ICollection<ProductGenre>? Genres { get; set; }
    public ICollection<ProductMinimalSystemRequirements>? MinimumSystemRequirements { get; set; }
    public ICollection<ProductRecommendedSystemRequirements>? RecommendedSystemRequirements { get; set; }
}