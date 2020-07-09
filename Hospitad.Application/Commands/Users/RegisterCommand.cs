using Hospitad.Application.DTOs;
using Hospitad.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.Commands.Users
{
    public class RegisterCommand : Request<OperationResult>
    {
        public RegisterCommand(RequestInfo info) : base(info)
        {

        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
    }
}
