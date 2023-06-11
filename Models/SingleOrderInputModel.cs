using System.ComponentModel.DataAnnotations;

namespace ShopASP.Models;

public class SingleOrderInputModel
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
}