using Hospitad.Application.Models.Oganizations;
using Hospitad.Domain.Organizations;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospitad.Persistence.Helpers
{
    public static class QueryableFilterExtensions
    {
        public static IQueryable<Organization> ApplyFilter(this IQueryable<Organization> query, OrganizationFilter filter)
        {
            if(filter.Enabled != null)
            {
                query = query.Where(q => q.Enabled == filter.Enabled.Value);
            }

            if(!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(q => q.Title.ToLower().Contains(filter.Title.ToLower()));
            }

            if(filter.Id != null)
            {
                query = query.Where(q => q.Id == filter.Id);
            }

            return query;
        }
    }
}
