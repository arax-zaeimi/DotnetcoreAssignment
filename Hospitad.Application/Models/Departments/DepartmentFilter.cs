using Hospitad.Common.Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.Models.Departments
{
    public class DepartmentFilter : PaginationFilter
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public bool? Enabled { get; set; }
        public int? ParentDepartmentId { get; set; }
        public int? OrganizationId { get; set; }
    }
}
