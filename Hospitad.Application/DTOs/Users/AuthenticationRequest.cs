using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.DTOs.Users
{
    public class AuthenticationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
