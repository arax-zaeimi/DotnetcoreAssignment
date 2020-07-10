using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.DTOs.Departments
{
    public class CreateDepartmentDto
    {
        public string Title { get; set; }
        public int OrganizationId { get; set; }
        public int? ParentDepartmentId { get; set; }
    }
}
