using Hospitad.Application.DTOs;
using Hospitad.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.Commands.Departments
{
    public class EditDepartmentCommand : Request<OperationResult>
    {
        public EditDepartmentCommand(RequestInfo info) : base(info)
        {

        }

        public int DepartmentId { get; set; }
        public string Title { get; set; }
        public int? OrganizationId { get; set; }
        public bool? Enabled { get; set; }
        public int? ParentDepartmentId { get; set; }
    }
}
