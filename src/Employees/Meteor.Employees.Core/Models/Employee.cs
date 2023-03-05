namespace Meteor.Employees.Core.Models;

using Enums;

public class Employee
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name max length is 50.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name max length is 50.")]
    public string LastName { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = true)]
    [StringLength(50, ErrorMessage = "Middle name max length is 50.")]
    public string MiddleName { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required.")]
    [StringLength(250, ErrorMessage = "Email address max length is 250.")]
    [EmailAddress(ErrorMessage = "Email address is invalid.")]
    public string EmailAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"\d{10,13}", ErrorMessage = "Phone number must contain only numbers.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

    [Required, MaxLength(200)]
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

    public EmployeeStatus Status { get; set; }

    public List<StatusChangeReason> StatusChanges { get; set; } = new();

    public List<EmployeeField> CustomFields { get; set; } = new();
}
