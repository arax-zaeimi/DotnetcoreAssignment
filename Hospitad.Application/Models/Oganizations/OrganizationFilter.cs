using Hospitad.Common.Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.Models.Oganizations
{
    public class OrganizationFilter : PaginationFilter
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public bool? Enabled { get; set; }
    }
}
