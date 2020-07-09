namespace Hospitad.Common.Infrastructure.Pagination
{
    public class PaginationFilter
    {
        private const int MinPageNumber = 1;
        private const int MinPageSize = 1;
        private const int MaxPageSize = 200;
            
        private int _page;
        private int _pageSize;

        protected PaginationFilter()
        {
            _page = MinPageNumber;
            _pageSize = MinPageSize;
        }
        
        public int Page
        {
            get => _page;
            set => _page = value > 0 ? value : MinPageNumber;
        }
        
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value < MinPageSize)
                    _pageSize = MinPageSize;
                else if (value > MaxPageSize)
                    _pageSize = MaxPageSize; // To avoiding performance issue
                else
                    _pageSize = value;
            }
        }
    }
}