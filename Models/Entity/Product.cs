using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopASP.Models.Entity;

public class Product : BaseNamed
{
    [MaxLength(1000)]
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Units { get; set; }
    public Platform Platform { get; set; }
    public Developer Developer { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public Image FrontCover;
    public List<Image>? Screenshots;
    public ICollection<Category>? Categories { get; set; }
    public ICollection<SystemRequirement>? MinimumSystemRequirements { get; set; }
    public ICollection<SystemRequirement>? RecommendedSystemRequirements { get; set; }
}