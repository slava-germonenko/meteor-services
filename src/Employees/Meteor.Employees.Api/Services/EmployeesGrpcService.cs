using Grpc.Core;
using MapsterMapper;
using Meteor.Employees.Api.Grpc;
using Meteor.Employees.Core.Dtos;
using Meteor.Employees.Core.Services;

namespace Meteor.Employees.Api.Services;

public class EmployeesGrpcService : EmployeesService.EmployeesServiceBase
{
    private readonly EmployeeSetupService _employeeSetupService;

    private readonly IMapper _mapper;

    public EmployeesGrpcService(EmployeeSetupService employeeSetupService, IMapper mapper)
    {
        _employeeSetupService = employeeSetupService;
        _mapper = mapper;
    }

    public override async Task<EmployeeResponse> CreateEmployee(CreateEmployeeRequest request, ServerCallContext context)
    {
        var employeeDto = _mapper.Map<CreateEmployeeDto>(request);
        var employee = await _employeeSetupService.CreateEmployeeAsync(employeeDto);
        return _mapper.Map<EmployeeResponse>(employee);
    }
}