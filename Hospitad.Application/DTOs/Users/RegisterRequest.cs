using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.DTOs.Users
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
    }
}
