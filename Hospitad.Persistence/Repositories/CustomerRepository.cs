using Hospitad.Application.Interfaces.Repositories;
using Hospitad.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Persistence.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {

        public CustomerRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
