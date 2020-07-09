using Hospitad.Application.Commands.Organizations;
using Hospitad.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.Factories.Organizations
{
    public interface IOrganizationFactory
    {
        Organization CreateOrganization(CreateOrganizationCommand createOrganizationCommand);
    }
}
