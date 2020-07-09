using Hospitad.Application.DTOs;
using Hospitad.Application.Models;
using Hospitad.Application.Models.Oganizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.Queries.Organizations
{
    public class GetOrganizationsQuery : Request<OperationResult>
    {
        public GetOrganizationsQuery(RequestInfo info) : base(info)
        {

        }

        public OrganizationFilter Filter { get; set; }
    }
}
