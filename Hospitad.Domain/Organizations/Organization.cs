using Hospitad.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Domain.Organizations
{
    public class Organization : BaseEntity, IEntity
    {
        public Organization()
        {
            Departments = new HashSet<Department>();
        }
        public string Title { get; set; }
        public bool Enabled { get; set; }

        #region Navigation Properties
        
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public ICollection<Department> Departments { get; set; }
        #endregion
    }
}
