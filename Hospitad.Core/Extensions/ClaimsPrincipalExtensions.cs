using Hospitad.Application.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hospitad.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUsername(this ClaimsPrincipal principal)
        {
            var idClaim = principal.Claims
                .SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (idClaim == null)
                return null;

            return idClaim.Value;
        }

        public static RequestInfo GetRequestInfo(this ClaimsPrincipal principal, HttpRequest request)
        {
            return new RequestInfo
            {   
                UserName = principal.GetUsername(),
                UserRoles = principal.GetRoles().ToList(),
                IpAddress = request.GetIpAddress()
            };
        }

        public static IEnumerable<string> GetRoles(this ClaimsPrincipal principal)
        {
            return principal.Claims
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value);
        }        
    }
}
