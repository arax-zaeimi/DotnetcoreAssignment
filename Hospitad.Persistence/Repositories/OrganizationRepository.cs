using Hospitad.Application.Interfaces.Repositories;
using Hospitad.Application.Models.Oganizations;
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
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        private readonly IQueryable<Organization> _queryableDbSet;

        public OrganizationRepository(DbContext dbContext) : base(dbContext)
        {
            _queryableDbSet = _dbContext.Set<Organization>();
        }

        public async Task<IList<Organization>> GetAllByFilterAsync(OrganizationFilter filter)
        {
            var query = _queryableDbSet;

            // Includes
            query = query.Include(x => x.Departments);

            query = query.ApplyFilter(filter);

            return await query.ToPaginatedListAsync(filter.Page, filter.PageSize);
        }
    }
}
