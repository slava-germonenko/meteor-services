using Meteor.Employees.Core;
using Microsoft.EntityFrameworkCore;

namespace Meteor.Employees.Api.Extensions;

public static class WebApplicationExtensions
{
    public static async Task ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<EmployeesContext>();
        await context.Database.MigrateAsync();
    }
}