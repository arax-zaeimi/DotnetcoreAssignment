using Hospitad.Application.DTOs;
using Hospitad.Application.Interfaces;
using Hospitad.Application.Queries.Organizations;
using Hospitad.Domain.Organizations;
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
    public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetOrganizationsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
        {
            var username = request.GetUserName();

            var customer = await _unitOfWork.Users.GetAll(false).Where(q => q.Username == username).Select(q => q.Customer).FirstOrDefaultAsync();
            if (customer == null)
            {
                return new OperationResult(result: false, statusCode: 400, message: $"No corresponding customer found for user: {request.GetUserName()}", value: null);
            }
            request.Filter.CustomerId = customer.Id;

            var entities = await _unitOfWork.Organizations.GetAllByFilterAsync(request.Filter);

            var result = new ListResult<Organization>
            {
                Page = request.Filter.Page,
                PageSize = request.Filter.PageSize,
                Data = entities,
            };

            return new OperationResult(true, 200, message: "", value: result);
        }
    }
}
