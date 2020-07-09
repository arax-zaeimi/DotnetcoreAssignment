using Hospitad.Application.Interfaces.Repositories;
using Hospitad.Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Persistence.Repositories
{
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
