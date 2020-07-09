using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospitad.Api.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetIpAddress(this HttpRequest request)
        {
            return request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        }
    }
}
