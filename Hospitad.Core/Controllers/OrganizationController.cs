using Hospitad.Api.Extensions;
using Hospitad.Application.Keys;
using Hospitad.Application.Models.Oganizations;
using Hospitad.Application.Queries.Organizations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospitad.Api.Controllers
{
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("organizations")]
        public async Task<IActionResult> GetOrganizations(OrganizationFilter filter)
        {
            var operation = await _mediator.Send(new GetOrganizationsQuery(User.GetRequestInfo(Request))
            {
                Filter = filter
            });

            // Result
            if (operation.Failed)
                return this.ReturnErrorResponse(operation, Messages.GetOrganizationsFailed);

            return Ok(operation.Value);
        }
    }
}
