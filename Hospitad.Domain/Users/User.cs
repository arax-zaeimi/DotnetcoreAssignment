using Hospitad.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Domain.Users
{
    public class User : BaseEntity, IEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
        public string UserRole { get; set; }

        #region Navigation Properties
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        #endregion
    }
}
