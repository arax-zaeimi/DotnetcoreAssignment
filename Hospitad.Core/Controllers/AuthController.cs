using Hospitad.Api.Extensions;
using Hospitad.Application.Commands.Users;
using Hospitad.Application.DTOs.Users;
using Hospitad.Application.Keys;
using Hospitad.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospitad.Api.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Login(AuthenticationRequest authentication)
        {
            var operation = await _mediator.Send(new UserLoginQuery(User.GetRequestInfo(Request)) { Username = authentication.Username, Password = authentication.Password });

            // Result
            if (operation.Failed)
            {
                return this.ReturnErrorResponse(operation, Messages.LoginFailed);
            }

            return Ok(operation.Value);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var operation = await _mediator.Send(new RegisterCommand(User.GetRequestInfo(Request)) 
            {
                ConfirmPassword = registerRequest.ConfirmPassword, 
                Password = registerRequest.Password, 
                Email = registerRequest.Email, 
                Fullname = registerRequest.Fullname, 
                Username = registerRequest.Username 
            });

            // Result
            if (operation.Failed)
            {
                return this.ReturnErrorResponse(operation, Messages.RegisterFailed);
            }

            operation = await _mediator.Send(new UserLoginQuery(User.GetRequestInfo(Request)) { Username = registerRequest.Username, Password = registerRequest.Password });
            
            // Result
            if (operation.Failed)
            {
                return this.ReturnErrorResponse(operation, Messages.RegisteredUserLoginFailed);
            }

            return Ok(operation.Value);
        }
    }
}
