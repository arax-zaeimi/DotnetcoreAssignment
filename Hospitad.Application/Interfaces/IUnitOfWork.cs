using Hospitad.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hospitad.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IOrganizationRepository Organizations { get; }
        IDepartmentRepository Departments { get; }
        IUserRepository Users { get; }

        Task<bool> SaveChangesAsync();
    }
}
