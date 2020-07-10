using Hospitad.Application.Commands.Departments;
using Hospitad.Application.DTOs;
using Hospitad.Application.DTOs.Departments;
using Hospitad.Application.Interfaces;
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
    public class EditDepartmentCommandHandler : IRequestHandler<EditDepartmentCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(EditDepartmentCommand request, CancellationToken cancellationToken)
        {
            var username = request.GetUserName();

            var customer = await _unitOfWork.Users.GetAll(false).Where(q => q.Username == username).Select(q => q.Customer).FirstOrDefaultAsync();
            if (customer == null)
            {
                return new OperationResult(result: false, statusCode: 400, message: $"No corresponding customer found for user: {request.GetUserName()}", value: null);
            }

            var department = await _unitOfWork.Departments.GetAll(true)
                .Include(q => q.SubDepartments)
                .Where(q => q.Organization.CustomerId == customer.Id && q.Id == request.DepartmentId)
                .FirstOrDefaultAsync();

            if (department == null)
            {
                return new OperationResult(result: false, statusCode: 400, message: $"Invalid DepartmentId", value: null);
            }

            department.UpdatedAt = DateTime.Now;
            
            if(!string.IsNullOrEmpty(request.Title))
            {
                department.Title = request.Title;
            }

            department.ParentDepartmentId = request.ParentDepartmentId;
            department.OrganizationId = request.OrganizationId.Value;

            if(request.Enabled.HasValue)
            {
                department.Enabled = request.Enabled.Value;
            }

            _unitOfWork.Departments.Update(department);

            var departmentDto = new DepartmentDto()
            {
                Id = department.Id,
                OrganizationId = department.OrganizationId,
                ParentDepartmentId = department.ParentDepartmentId,
                Title = department.Title
            };
            
            if (await _unitOfWork.SaveChangesAsync())
            {
                return new OperationResult(result: true, statusCode: 200, message: $"Department '{departmentDto.Title}' Updated", value: departmentDto, entityId: departmentDto.Id);
            }
            else
            {
                return new OperationResult(result: false, statusCode: 500, message: $"Department Update Failed.", value: null);
            }
        }
    }
}
