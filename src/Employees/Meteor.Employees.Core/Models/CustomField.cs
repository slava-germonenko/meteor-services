namespace Meteor.Employees.Core.Models;

using Enums;

public class CustomField
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required.")]
    [StringLength(70, ErrorMessage = "Name max length is 70.")]
    public string Name { get; set; } = string.Empty;

    public bool Required { get; set; }

    public bool Unique { get; set; }

    public FieldTypes Type { get; set; }
}
