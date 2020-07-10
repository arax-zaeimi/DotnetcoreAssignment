using Hospitad.Application.Commands.Departments;
using Hospitad.Application.DTOs;
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
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
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

            if(department == null)
            {
                return new OperationResult(result: false, statusCode: 400, message: $"Invalid DepartmentId", value: null);
            }

            if(department.SubDepartments.Any())
            {
                return new OperationResult(
                    result: false, 
                    statusCode: 400, 
                    message: $"There are departments bound to this department. Please make sure there are no child departments defined and then delete this department again.", 
                    value: null);
            }

            _unitOfWork.Departments.Remove(department);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return new OperationResult(result: true, statusCode: 200, message: $"Department '{department.Title}' Deleted", value: null);
            }
            else
            {
                return new OperationResult(result: false, statusCode: 500, message: $"Department Delete Failed.", value: null);
            }
        }
    }
}
