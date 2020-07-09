using Hospitad.Application.Commands.Organizations;
using Hospitad.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.Factories.Organizations
{
    public class OrganizationFactory : IOrganizationFactory
    {
        public Organization CreateOrganization(CreateOrganizationCommand createOrganizationCommand)
        {
            return new Organization()
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = createOrganizationCommand.Title,
            };
        }
    }
}
