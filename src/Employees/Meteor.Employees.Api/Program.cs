using Meteor.Employees.Api.Extensions;
using Meteor.Employees.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables("METEOR_");

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var connectionString = builder.Configuration.GetConnectionString("Core");
builder.Services.AddDbContext<EmployeesContext>(
    options => options
        .UseNpgsql(connectionString, opt => opt.MigrationsAssembly("Meteor.Employees.Infrastructure.Migrations"))
        .UseSnakeCaseNamingConvention()
);

var app = builder.Build();

var applyMigrations = builder.Configuration.GetValue<bool>("Migrations:RunOnStartup");
if (applyMigrations)
{
    await app.ApplyDatabaseMigrations();
}

app.MapGrpcReflectionService();
app.Run();
