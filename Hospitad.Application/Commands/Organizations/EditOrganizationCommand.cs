using Hospitad.Application.DTOs;
using Hospitad.Application.DTOs.Organizations;
using Hospitad.Application.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Hospitad.Application.Commands.Organizations
{
    public class EditOrganizationCommand : Request<OperationResult>
    {
        public EditOrganizationCommand(RequestInfo info) : base(info)
        {
            
        }

        public int OrganizationId { get; set; }
        public string Title { get; set; }
    }
}
