using Hospitad.Application.DTOs;
using Hospitad.Application.Models;
using Hospitad.Application.Models.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.Queries.Departments
{
    public class GetDepartmentsQuery : Request<OperationResult>
    {
        public GetDepartmentsQuery(RequestInfo info) : base(info)
        {

        }

        public DepartmentFilter Filter { get; set; }
    }
}
