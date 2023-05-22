namespace ShopASP.Models.Entity;

public class ProductGenre : Base
{
    public Product Product { get; set; }
    public Genre Genre { get; set; }
}