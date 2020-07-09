using Hospitad.Domain.Organizations;
using Hospitad.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hospitad.Domain.Customers
{
    public class Customer : BaseEntity, IEntity
    {
        public Customer()
        {
            Users = new HashSet<User>();
            Organizations = new HashSet<Organization>();
        }

        public string Title { get; set; }
        public bool Enabled { get; set; }


        #region Navigation Properties
        public ICollection<Organization> Organizations { get; set; } 

        public ICollection<User> Users { get; set; }
        #endregion
    }
}
