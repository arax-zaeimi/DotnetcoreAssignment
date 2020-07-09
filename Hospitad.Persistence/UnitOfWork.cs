using Hospitad.Application.Interfaces;
using Hospitad.Application.Interfaces.Repositories;
using Hospitad.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hospitad.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        private ICustomerRepository _customers;
        public ICustomerRepository Customers 
        {
            get
            {
                if(_customers == null)
                {
                    _customers = new CustomerRepository(_dbContext);
                }

                return _customers;
            }
        }

        
        
        private IOrganizationRepository _organizations;
        public IOrganizationRepository Organizations 
        {
            get
            {
                if(_organizations == null)
                {
                    _organizations = new OrganizationRepository(_dbContext);
                }

                return _organizations;
            }
        }


        private IDepartmentRepository _departments;
        public IDepartmentRepository Departments 
        { 
            get
            {
                if(_departments == null)
                {
                    _departments = new DepartmentRepository(_dbContext);
                }

                return _departments;
            }
        }

        private IUserRepository _users;
        public IUserRepository Users
        {
            get
            {
                if(_users == null)
                {
                    _users = new UserRepository(_dbContext);
                }

                return _users;
            }
        }

        public void Dispose()
        {
            if(_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
