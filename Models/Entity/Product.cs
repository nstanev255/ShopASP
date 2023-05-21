namespace ShopASP.Models.Entity;

public class Product : Base
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Platform? Platform { get; set; }
    public Developer? Developer { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public ICollection<Category>? Categories { get; set; }
}