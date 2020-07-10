using Hospitad.Application.Models.Departments;
using Hospitad.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hospitad.Application.Interfaces.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<IList<Department>> GetAllByFilterAsync(DepartmentFilter filter);
    }
}
