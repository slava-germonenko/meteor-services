using Meteor.Common.Configuration.Extensions;
using Meteor.Common.Core.Services.Abstractions;
using Meteor.Common.Grpc.Interceptors;
using Meteor.Common.Messaging.RabbitMq.Extensions;
using Meteor.Employees.Api.Extensions;
using Meteor.Employees.Api.Services;
using Meteor.Employees.Core;
using Meteor.Employees.Core.Contracts;
using Meteor.Employees.Core.Dtos;
using Meteor.Employees.Core.Models;
using Meteor.Employees.Core.Services;
using Meteor.Employees.Core.Services.Abstractions;
using Meteor.Employees.Core.Services.Validators;
using Meteor.Employees.Infrastructure;
using Meteor.Employees.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables("METEOR_");

builder.Services.Configure<PasswordsOptions>(builder.Configuration.GetSection("Security:Passwords"));

builder.Services.AddMapper();
builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<ExceptionsLoggingInterceptor>();
});
builder.Services.AddGrpcReflection();

var connectionString = builder.Configuration.GetConnectionString("Core");
builder.Services.AddDbContext<EmployeesContext>(
    options => options
        .UseNpgsql(connectionString, opt => opt.MigrationsAssembly("Meteor.Employees.Infrastructure.Migrations"))
        .UseSnakeCaseNamingConvention()
);

builder.Services.AddChannelFactory(options =>
{
    options.Host = builder.Configuration.GetRequiredValue<string>("Messaging:Host");
    options.User = builder.Configuration.GetRequiredValue<string>("Messaging:User");
    options.Password = builder.Configuration.GetRequiredValue<string>("Messaging:Password");
});

builder.Services.AddPublisher<EmployeeNotification>(options =>
{
    options.ExchangeName = builder.Configuration
        .GetRequiredValue<string>("Messaging:Exchanges:EmployeeChanged:ExchangeName");
    
    options.RoutingKey = builder.Configuration
        .GetRequiredValue<string>("Messaging:Exchanges:EmployeeChanged:RoutingKey");
});

builder.Services.AddPublisher<PasswordUpdatedNotification>(options =>
{
    options.ExchangeName = builder.Configuration
        .GetRequiredValue<string>("Messaging:Exchanges:EmployeeChangedPassword:ExchangeName");
    
    options.RoutingKey = builder.Configuration
        .GetRequiredValue<string>("Messaging:Exchanges:EmployeeChangedPassword:RoutingKey");
});

builder.Services.AddScoped<IEmployeeSetupService, EmployeeSetupService>();
builder.Services.AddScoped<IEmployeeManagementService, EmployeeManagementService>();
builder.Services.AddScoped<IPasswordsService, PasswordsService>();
builder.Services.AddScoped<IPasswordHasher, Pbkdf2PasswordHasher>();
builder.Services.AddScoped<IAsyncValidator<Employee>, EmployeeFieldsValidator>();
builder.Services.AddScoped<IAsyncValidator<Employee>, EmployeeUniquenessValidator>();

var app = builder.Build();

var applyMigrations = builder.Configuration.GetValue<bool>("Migrations:RunOnStartup");
if (applyMigrations)
{
    await app.ApplyDatabaseMigrations();
}

app.MapGrpcReflectionService();
app.MapGrpcService<EmployeesGrpcService>();
app.Run();
