using Hospitad.Application.Commands.Departments;
using Hospitad.Application.DTOs;
using Hospitad.Application.DTOs.Departments;
using Hospitad.Application.Interfaces;
using Hospitad.Domain.Organizations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hospitad.Application.Handlers.Departments
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (request.OrganizationId < 1)
            {
                return new OperationResult(result: false, statusCode: 400, message: $"Invalid Organization", value: null);
            }


            var username = request.GetUserName();

            var customer = await _unitOfWork.Users.GetAll(false).Where(q => q.Username == username).Select(q => q.Customer).FirstOrDefaultAsync();
            if (customer == null)
            {
                return new OperationResult(result: false, statusCode: 400, message: $"No corresponding customer found for user: {request.GetUserName()}", value: null);
            }

            var organization = await _unitOfWork.Organizations.GetAll(true).Where(q => q.Id == request.OrganizationId).FirstOrDefaultAsync(cancellationToken);
            if (organization == null)
            {
                return new OperationResult(result: false, statusCode: 400, message: $"Invalid Organization", value: null);
            }

            //The parent department should be valid and related to the current organization
            if (request.ParentDepartmentId != null)
            {
                var isParentValid = await _unitOfWork.Departments.GetAll(false).AnyAsync(q => q.OrganizationId == organization.Id && q.Id == request.ParentDepartmentId);
                if (!isParentValid)
                {
                    return new OperationResult(result: false, statusCode: 400, message: $"Invalid Parent Department", value: null);
                }
            }

            var department = new Department()
            {
                Enabled = true,
                ParentDepartmentId = request.ParentDepartmentId,
                OrganizationId = request.OrganizationId,
                Title = request.Title
            };

            _unitOfWork.Departments.Add(department);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return new OperationResult(result: true,
                    statusCode: 201,
                    message: $"Department Created with Id: {department.Id}",
                    value: new DepartmentDto() { Title = department.Title, Id = department.Id, OrganizationId = department.OrganizationId, ParentDepartmentId = department.ParentDepartmentId },
                    entityId: department.Id);
            }
            else
            {
                return new OperationResult(result: false, statusCode: 500, message: $"Department Creation Failed.", value: null);
            }
        }
    }
}
