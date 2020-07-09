using Hospitad.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospitad.Api.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ReturnErrorResponse(this ControllerBase controller, OperationResult operation, string defaultMessage = null)
        {
            return operation.StatusCode switch
            {
                400 => controller.BadRequest(operation.Message),
                422 => controller.UnprocessableEntity(operation.Message),
                _ => controller.UnprocessableEntity(defaultMessage),
            };
        }
    }
}
