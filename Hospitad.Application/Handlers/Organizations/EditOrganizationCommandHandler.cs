using Hospitad.Application.Commands.Organizations;
using Hospitad.Application.DTOs;
using Hospitad.Application.DTOs.Organizations;
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
    public class EditOrganizationCommandHandler : IRequestHandler<EditOrganizationCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditOrganizationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(EditOrganizationCommand request, CancellationToken cancellationToken)
        {
            var username = request.GetUserName();

            var customer = _unitOfWork.Users.GetAll(false).Where(q => q.Username == username).Select(q => q.Customer).FirstOrDefault();
            if (customer == null)
            {
                return new OperationResult(result: false, statusCode: 400, message: $"No corresponding customer found for user: {request.GetUserName()}", value: null);
            }

            var organization = _unitOfWork.Organizations
                .GetAll(true)
                .FirstOrDefault(q => q.Id == request.OrganizationId && q.Customer.Users.Any(u => u.Username == username));

            if(organization == null)
            {
                return new OperationResult(result: false, statusCode: 400, message: $"Organization not found", value: null);
            }

            organization.Title = request.Title;
            organization.UpdatedAt = DateTime.Now;

            _unitOfWork.Organizations.Update(organization);

            if(await _unitOfWork.SaveChangesAsync())
            {
                var organizationDto = new OrganizationDto()
                {
                    Id = organization.Id,
                    Title = organization.Title
                };

                return new OperationResult(result: true, statusCode: 200, message: $"Organization with Id: {organizationDto.Id} Updated", value: organizationDto, entityId: organizationDto.Id);
            }
            else
            {
                return new OperationResult(result: false, statusCode: 500, message: $"Organizatoin Update Failed.", value: null);
            }
        }
    }
}
