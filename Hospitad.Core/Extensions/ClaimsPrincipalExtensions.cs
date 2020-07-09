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
        public static int GetId(this ClaimsPrincipal principal)
        {
            var idClaim = principal.Claims
                .SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (idClaim == null)
                return -1;
            return int.Parse(idClaim.Value);
        }

        public static string GetUsername(this ClaimsPrincipal principal)
        {
            return principal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }

        public static RequestInfo GetRequestInfo(this ClaimsPrincipal principal, HttpRequest request)
        {
            return new RequestInfo
            {
                UserId = principal.GetId(),
                UserName = principal.GetUsername(),
                UserRoles = principal.GetRoles().ToList(),
                //ManagingAcademyId = principal.GetManagedAcademyId(),
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
