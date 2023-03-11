using Meteor.Employees.Core.Models;

namespace Meteor.Employees.Core.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<Employee> IncludeAll(this IQueryable<Employee> query)
        => query
            .Include(e => e.CustomFields)
            .Include(e => e.StatusChanges);
}