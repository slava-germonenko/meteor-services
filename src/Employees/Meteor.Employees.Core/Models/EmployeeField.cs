namespace Meteor.Employees.Core.Models;

public class EmployeeField
{
    public CustomField Field { get; set; }

    public string Value { get; set; }

    protected EmployeeField()
    {
        Value = string.Empty;
        Field = new();
    }

    public EmployeeField(CustomField field, string value)
    {
        Field = field;
        Value = value;
    }
}