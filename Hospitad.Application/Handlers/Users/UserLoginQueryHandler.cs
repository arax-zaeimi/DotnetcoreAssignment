using Hospitad.Application.DTOs;
using Hospitad.Application.Interfaces;
using Hospitad.Application.Interfaces.Repositories;
using Hospitad.Application.Queries.Users;
using Hospitad.Domain.Users;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hospitad.Application.Handlers.Users
{
    public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, OperationResult>
    {
        IUserRepository _userRepository;
        IConfiguration _configuration;
        public UserLoginQueryHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public Task<OperationResult> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetAll().FirstOrDefault(q => q.Username == request.Username && q.Password == request.Password && q.Enabled);
            if(user == null)
            {   
                return Task.FromResult(new OperationResult(false, 401, $"Authntication failed for '{request.Username}'", value: null));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("role", user.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var result = new OperationResult(true, 200, message: $"User '{user.Username}' Autheticated", value: jwtToken);

            return Task.FromResult(result);
        }
    }
}
