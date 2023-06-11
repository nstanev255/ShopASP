using ShopASP.Models.Entity;

namespace ShopASP.Utils;

public static class PriceUtils
{
    public static List<decimal> OrderProductPrices(List<OrderProduct> orderProducts)
    {
        var prices = new List<decimal>();
        if (orderProducts.Count == 0)
        {
            return prices;
        }

        foreach (var orderProduct in orderProducts)
        {
            prices.Add(orderProduct.Price);
        }

        return prices;
    }

    public static decimal CalculateFinalPrice(List<decimal> productPrices)
    {
        decimal finalPrice = 0.00M;

        foreach (var price in productPrices)
        {
            finalPrice += price;
        }

        return finalPrice;

    }

    public static List<decimal> ProductPrices(List<Product> products)
    {
        var prices = new List<decimal>();
        if (products.Count == 0)
        {
            return prices;
        }

        foreach (var product in products)
        {
            prices.Add(product.Price);
        }

        return prices;
    }
}