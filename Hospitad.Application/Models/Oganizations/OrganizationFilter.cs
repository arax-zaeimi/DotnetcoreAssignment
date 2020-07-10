using Hospitad.Common.Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Hospitad.Application.Models.Oganizations
{
    public class OrganizationFilter : PaginationFilter
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public bool? Enabled { get; set; }
        [JsonIgnore]
        public int? CustomerId { get; set; }
    }
}
