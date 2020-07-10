using Hospitad.Application.Commands.Organizations;
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

namespace Hospitad.Application.Handlers.Organizations
{
    public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteOrganizationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            var username = request.GetUserName();

            var customer = _unitOfWork.Users.GetAll(false).Where(q => q.Username == username).Select(q => q.Customer).FirstOrDefault();
            if (customer == null)
            {
                return new OperationResult(result: false, statusCode: 400, message: $"No corresponding customer found for user: {request.GetUserName()}", value: null);
            }

            var organization = _unitOfWork.Organizations.GetAll(true)
                .Include(q => q.Customer)
                .Include(q => q.Departments)
                .FirstOrDefault(q => q.Id == request.OrganizationId && q.Customer.Users.Any(u => u.Username == username));

            if (organization == null)
            {
                return new OperationResult(result: false, statusCode: 400, message: $"Organization not found", value: null);
            }

            if (organization.Departments.Any())
            {
                return new OperationResult(result: false, statusCode: 400, message: $"Organization has some departments. Please delete departments first.", value: null);
            }

            _unitOfWork.Organizations.Remove(organization);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return new OperationResult(result: true, statusCode: 200, message: $"Organization with Id: {organization.Id} Deleted", value: organization, entityId: organization.Id);
            }
            else
            {
                return new OperationResult(result: false, statusCode: 500, message: $"Organizatoin Delete Failed.", value: null);
            }
        }
    }
}
