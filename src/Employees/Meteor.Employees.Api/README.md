# Employees Microservice

## Migrations

To apply create a new migration enter the following command from within src/Employees folder
```
dotnet ef migrations add {MIGRATION_NAME_PLACEHOLDER} --project Meteor.Employees.Infrastructure.Migrations --startup-project Meteor.Employees.Api
```
