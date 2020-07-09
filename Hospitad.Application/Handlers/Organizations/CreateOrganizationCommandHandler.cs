﻿using Hospitad.Application.Commands.Organizations;
using Hospitad.Application.DTOs;
using Hospitad.Application.DTOs.Organizations;
using Hospitad.Application.Factories.Organizations;
using Hospitad.Application.Interfaces;
using Hospitad.Domain.Organizations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hospitad.Application.Handlers.Organizations
{
    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrganizationFactory _organizationFactory;
        public CreateOrganizationCommandHandler(IUnitOfWork unitOfWork, IOrganizationFactory organizationFactory)
        {
            _unitOfWork = unitOfWork;
            _organizationFactory = organizationFactory;
        }
        public async Task<OperationResult> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var userId = request.GetUserId();

            var customer = _unitOfWork.Users.GetAll(false).Where(q => q.Id == userId).Select(q => q.Customer).FirstOrDefault();
            if (customer == null)
            {
                return new OperationResult(result: false, statusCode: 400, message: $"No corresponding customer found for user: {request.GetUserName()}", value: null);
            }

            var organization = _organizationFactory.CreateOrganization(request);
            organization.CustomerId = customer.Id;
            organization.Enabled = true;

            _unitOfWork.Organizations.Add(organization);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return new OperationResult(result: true, statusCode: 201, message: $"Organization Created with Id: {organization.Id}", value: organization, entityId: organization.Id);
            }
            else
            {
                return new OperationResult(result: false, statusCode: 500, message: $"Organizatoin Creation Failed.", value: null);
            }
        }
    }
}