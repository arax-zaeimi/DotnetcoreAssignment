using Hospitad.Application.DTOs;
using Hospitad.Application.Interfaces;
using Hospitad.Application.Queries.Organizations;
using Hospitad.Domain.Organizations;
using MediatR;
using System;
using System.Collections.Generic;
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
