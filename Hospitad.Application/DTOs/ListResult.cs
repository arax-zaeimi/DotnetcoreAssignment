using Hospitad.Common.Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.DTOs
{
    public class ListResult<T> : PaginationResult
    {
        public IList<T> Data { get; set; }
    }
}
