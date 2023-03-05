using Meteor.Employees.Core;
using Microsoft.EntityFrameworkCore;

namespace Meteor.Employees.Api.Extensions;

public static class WebApplicationExtensions
{
    public static async Task ApplyDatabaseMigrations(this WebApplication app)
    {
        var context = app.Services.GetRequiredService<EmployeesContext>();
        await context.Database.MigrateAsync();
    }
}