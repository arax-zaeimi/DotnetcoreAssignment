using Hospitad.Application.DTOs;
using Hospitad.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.Commands.Departments
{
    public class DeleteDepartmentCommand : Request<OperationResult>
    {
        public DeleteDepartmentCommand(RequestInfo info) : base(info)
        {

        }

        public int DepartmentId { get; set; }
    }
}
