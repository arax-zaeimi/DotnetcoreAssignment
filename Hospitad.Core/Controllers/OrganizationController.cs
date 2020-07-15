using Hospitad.Api.Extensions;
using Hospitad.Application.Commands.Organizations;
using Hospitad.Application.DTOs.Organizations;
using Hospitad.Application.Keys;
using Hospitad.Application.Models.Oganizations;
using Hospitad.Application.Queries.Organizations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospitad.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganizations([FromQuery] OrganizationFilter filter)
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

        [HttpPost]
        public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationDto command)
        {
            var operation = await _mediator.Send(new CreateOrganizationCommand(User.GetRequestInfo(Request)) { Title = command.Title });

            // Result
            if (operation.Failed)
                return this.ReturnErrorResponse(operation, Messages.CreateOrganizationFailed);

            return Created("", operation.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganization([FromRoute(Name = "id")] int organizationId, EditOrganizationDto command)
        {
            var operation = await _mediator.Send(new EditOrganizationCommand(User.GetRequestInfo(Request))
            {
                Title = command.Title,
                OrganizationId = organizationId
            });

            // Result
            if (operation.Failed)
                return this.ReturnErrorResponse(operation, Messages.EditOrganizationFailed);

            return Ok(operation.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization([FromRoute(Name = "id")] int organizationId)
        {
            var operation = await _mediator.Send(new DeleteOrganizationCommand(User.GetRequestInfo(Request))
            {
                OrganizationId = organizationId
            });

            // Result
            if (operation.Failed)
                return this.ReturnErrorResponse(operation, Messages.EditOrganizationFailed);

            return Ok(operation.Message);
        }
    }
}
