using Grpc.Core;
using MapsterMapper;
using Meteor.Employees.Api.Grpc;
using Meteor.Employees.Core.Dtos;
using Meteor.Employees.Core.Services.Abstractions;

namespace Meteor.Employees.Api.Services;

public class EmployeesGrpcService : EmployeesService.EmployeesServiceBase
{
    private readonly IEmployeeSetupService _employeeSetupService;

    private readonly IEmployeeManagementService _employeeManagementService;

    private readonly IPasswordsService _passwordsService;

    private readonly IMapper _mapper;

    public EmployeesGrpcService(
        IEmployeeSetupService employeeSetupService,
        IEmployeeManagementService employeeManagementService,
        IPasswordsService passwordsService,
        IMapper mapper
    )
    {
        _employeeSetupService = employeeSetupService;
        _employeeManagementService = employeeManagementService;
        _passwordsService = passwordsService;
        _mapper = mapper;
    }

    public override async Task<EmployeeResponse> CreateEmployee(
        CreateEmployeeRequest request,
        ServerCallContext context
    )
    {
        var employeeDto = _mapper.Map<CreateEmployeeDto>(request);
        var employee = await _employeeSetupService.CreateEmployeeAsync(employeeDto);
        return _mapper.Map<EmployeeResponse>(employee);
    }

    public override async Task<EmployeeResponse> UpdateEmployee(
        UpdateEmployeeRequest request,
        ServerCallContext context
    )
    {
        var employeeDto = _mapper.Map<UpdateEmployeeDto>(request);
        var employee = await _employeeManagementService.UpdateEmployeeDetailsAsync(request.Id, employeeDto);
        return _mapper.Map<EmployeeResponse>(employee);
    }

    public override async Task<PasswordSetResponse> SetEmployeePassword(
        SetPasswordRequest request,
        ServerCallContext context
    )
    {
        await _passwordsService.SetPasswordAsync(request.UserId, request.Password);
        return new ();
    }
}