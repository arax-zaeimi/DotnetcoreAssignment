namespace Hospitad.Common.Infrastructure.Pagination
{
    public static class PaginationHelper
    {
        public static TFilter CorrectPaginationFilter<TFilter>(this TFilter filter) where TFilter : PaginationFilter
        {
            if (filter.Page < 1)
                filter.Page = 1;

            if (filter.PageSize < 10)
                filter.PageSize = 10;
            else if (filter.PageSize > 100)
                filter.PageSize = 100;

            return filter;
        }
    }
}