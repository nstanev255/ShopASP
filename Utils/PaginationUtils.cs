namespace ShopASP.Utils;

using ShopASP.Constants;

public static class PaginationUtils
{
    public static int CalculateOffset(int page)
    {
        if (page <= 1)
        {
            return 0;
        }

        return page * Constants.ItemsPerPage;
    }
}