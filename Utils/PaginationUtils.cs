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

    public static int CalculatePageNumber(int items)
    {
        return (items + Constants.ItemsPerPage - 1) / Constants.ItemsPerPage;
    }

    public static List<int> GetPaginationLimits(int page, int totalPages)
    {
        var start = page - 2;
        var end = page + 2;

        if (end > totalPages)
        {
            start -= (end - totalPages);
            end = totalPages;
        }

        if (start <= 0)
        {
            end += ((start - 1) * (-1));
            start = 1;
        }

        end = end > totalPages ? totalPages : end;

        return new List<int> { start, end };
    }
}