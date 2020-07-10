using Hospitad.Api.Extensions;
using Hospitad.Application.Commands.Departments;
using Hospitad.Application.DTOs.Departments;
using Hospitad.Application.Keys;
using Hospitad.Application.Models.Departments;
using Hospitad.Application.Queries.Departments;
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
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments([FromQuery] DepartmentFilter filter)
        {
            var operation = await _mediator.Send(new GetDepartmentsQuery(User.GetRequestInfo(Request))
            {
                Filter = filter
            });

            // Result
            if (operation.Failed)
                return this.ReturnErrorResponse(operation, Messages.GetDepartmentsFailed);

            return Ok(operation.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto command)
        {
            var operation = await _mediator.Send(new CreateDepartmentCommand(User.GetRequestInfo(Request)) 
            { 
                Title = command.Title,
                OrganizationId = command.OrganizationId,
                ParentDepartmentId = command.ParentDepartmentId
            });

            // Result
            if (operation.Failed)
                return this.ReturnErrorResponse(operation, Messages.CreateDepartmentFailed);

            return Ok(operation.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment([FromBody]EditDepartmentDto department, [FromRoute(Name = "id")]int departmentId)
        {
            var operation = await _mediator.Send(new EditDepartmentCommand(User.GetRequestInfo(Request))
            {
                Title = department.Title,
                OrganizationId = department.OrganizationId,
                ParentDepartmentId = department.ParentDepartmentId,
                DepartmentId = departmentId,
                Enabled = department.Enabled
            });

            // Result
            if (operation.Failed)
                return this.ReturnErrorResponse(operation, Messages.EditDepartmentFailed);

            return Ok(operation.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute(Name = "id")] int departmentId)
        {
            var operation = await _mediator.Send(new DeleteDepartmentCommand(User.GetRequestInfo(Request))
            {
                DepartmentId = departmentId
            });

            // Result
            if (operation.Failed)
                return this.ReturnErrorResponse(operation, Messages.DeleteDepartmentFailed);

            return Ok(operation.Value);
        }
    }
}
