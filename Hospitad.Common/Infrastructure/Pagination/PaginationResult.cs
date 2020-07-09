namespace Hospitad.Common.Infrastructure.Pagination
{
    public class PaginationResult
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public int TotalPages =>
            TotalCount % PageSize == 0 ? TotalCount / PageSize : TotalCount / PageSize + 1;
        public bool HasPreviousPage => Page > 0;
        public bool HasNextPage => Page + 1 < TotalPages;
    }
}