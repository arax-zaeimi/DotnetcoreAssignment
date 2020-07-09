using Hospitad.Application.Models.Oganizations;
using Hospitad.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hospitad.Application.Interfaces.Repositories
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task<IList<Organization>> GetAllByFilterAsync(OrganizationFilter filter);
    }
}
