namespace ShopASP.Models.Entity;

public class ProductRecommendedSystemRequirements : Base
{
    public Product Product { get; set; } = new();
    public SystemRequirement SystemRequirement { get; set; } = new();
}