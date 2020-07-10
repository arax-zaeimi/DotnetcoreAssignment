using Hospitad.Application.Interfaces.Repositories;
using Hospitad.Application.Models.Departments;
using Hospitad.Common.Extensions;
using Hospitad.Domain.Organizations;
using Hospitad.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospitad.Persistence.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly IQueryable<Department> _queryableDbSet;
        public DepartmentRepository(DbContext dbContext) : base(dbContext)
        {
            _queryableDbSet = _dbContext.Set<Department>();
        }

        public async Task<IList<Department>> GetAllByFilterAsync(DepartmentFilter filter)
        {
            var query = _queryableDbSet;

            // Includes
            query = query
                .Include(x => x.ParentDepartment)
                .Include(x => x.SubDepartments)
                .Include(x => x.Organization);

            query = query.ApplyFilter(filter);

            return await query.ToPaginatedListAsync(filter.Page, filter.PageSize);
        }
    }
}
