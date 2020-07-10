using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.DTOs.Departments
{
    public class EditDepartmentDto
    {   
        public string Title { get; set; }
        public int? OrganizationId { get; set; }
        public bool? Enabled { get; set; }
        public int? ParentDepartmentId { get; set; }
    }
}
