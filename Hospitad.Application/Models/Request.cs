using System.Collections.Generic;
using MediatR;

namespace Hospitad.Application.Models
{
    public class Request<T> : IRequest<T> where T : class
    {
        private string UserName { get; }
        private IList<string> UserRoles { get; }
        private string IpAddress { get; }        

        public Request(RequestInfo info)
        {
            UserName = info.UserName;
            UserRoles = info.UserRoles;
            IpAddress = info.IpAddress;
        }

        
        public string GetUserName()
        {
            return UserName;
        }
        
        public IList<string> GetUserRoles()
        {
            return UserRoles;
        }
        
        public string GetIpAddress()
        {
            return IpAddress;
        }
    }
}