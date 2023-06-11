namespace ShopASP.Utils;

public static class UUID
{
    public static string Generate()
    {
        return Guid.NewGuid().ToString();
    }
}