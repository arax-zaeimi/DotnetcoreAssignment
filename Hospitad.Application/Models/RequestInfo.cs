using System.Collections.Generic;

namespace Hospitad.Application.Models
{
    public class RequestInfo
    {
        public string UserName { get; set; }
        public IList<string> UserRoles { get; set; }
        public string IpAddress { get; set; }
    }
}