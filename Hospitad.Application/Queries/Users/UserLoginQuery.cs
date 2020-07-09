using Hospitad.Application.DTOs;
using Hospitad.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.Queries.Users
{
    public class UserLoginQuery : Request<OperationResult>
    {
        public UserLoginQuery(RequestInfo info) : base(info)
        {

        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
