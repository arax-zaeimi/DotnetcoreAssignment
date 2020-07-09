using Hospitad.Application.Commands.Users;
using Hospitad.Application.DTOs;
using Hospitad.Application.Interfaces;
using Hospitad.Domain.Customers;
using Hospitad.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hospitad.Application.Handlers.Users
{
    public class UserRegisterCommandHandler : IRequestHandler<RegisterCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserRegisterCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _unitOfWork.Users.ExistsAsync(q => q.Username.ToLower() == request.Username.ToLower());
            if (userExists)
            {
                return new OperationResult(false, 400, message: $"User '{request.Username}' already taken.", value: null);
            }

            var customer = new Customer()
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Enabled = true,
                Title = request.Fullname,
            };

            var user = new User()
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Email = request.Email,
                Password = request.Password,
                Username = request.Username,
                UserRole = "User",
                Enabled = true,
            };

            customer.Users.Add(user);

            _unitOfWork.Users.Add(user);
            _unitOfWork.Customers.Add(customer);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return new OperationResult(true, 201, $"User '{request.Username}' Created", value: user, entityId: user.Id);
            }
            else
            {
                return new OperationResult(false, 400, $"Registration Failed for '{request.Username}'", value: null);
            }
        }
    }
}
