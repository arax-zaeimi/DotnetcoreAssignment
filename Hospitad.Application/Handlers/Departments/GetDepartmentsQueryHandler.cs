using Hospitad.Application.DTOs;
using Hospitad.Application.DTOs.Departments;
using Hospitad.Application.Interfaces;
using Hospitad.Application.Queries.Departments;
using Hospitad.Domain.Organizations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hospitad.Application.Handlers.Departments
{
    public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDepartmentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var username = request.GetUserName();

            var customer = await _unitOfWork.Users.GetAll(false)
                .Where(q => q.Username == username)
                .Include(q => q.Customer).ThenInclude(q => q.Organizations)
                .Select(q => q.Customer).FirstOrDefaultAsync();

            if (customer == null)
            {
                return new OperationResult(result: false, statusCode: 400, message: $"No corresponding customer found for user: {request.GetUserName()}", value: null);
            }

            if (!customer.Organizations.Any())
            {
                return new OperationResult(true, 200, message: "No organizations defined for this user", value: null);
            }

            request.Filter.OrganizationIds = customer.Organizations.Select(q => q.Id).Distinct().ToList();

            var entities = await _unitOfWork.Departments.GetAllByFilterAsync(request.Filter);

            IList<DepartmentDto> departments = entities
                .Select(q => new DepartmentDto()
                {
                    Id = q.Id,
                    Title = q.Title,
                    OrganizationId = q.OrganizationId,
                    ParentDepartmentId = q.ParentDepartmentId
                }).ToList();

            var result = new ListResult<DepartmentDto>
            {
                Page = request.Filter.Page,
                PageSize = request.Filter.PageSize,
                Data = departments
            };

            return new OperationResult(true, 200, message: "", value: result);
        }
    }
}
