using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Domain.Organizations
{
    public class Department : BaseEntity, IEntity
    {
        public string Title { get; set; }
        public bool Enabled { get; set; }

        #region Navigation Properties
        //Any department should have an organization (for easier and faster queries)
        public Organization Organization { get; set; }
        public int OrganizationId { get; set; }

        //Each department can have a single parent department.
        public Nullable<int> ParentDepartmentId { get; set; }
        public Department ParentDepartment { get; set; }

        public ICollection<Department> SubDepartments { get; set; }
        #endregion
    }
}
