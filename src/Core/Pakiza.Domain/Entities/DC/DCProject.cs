using Pakiza.Domain.Enums;

namespace Pakiza.Domain.Entities.DC;

[Table(name: "DC_Project")]
public class DCProject : BaseEntity
{
    [Required, MaxLength(200)]
    public string ProjectName { get; set; } = string.Empty;

    public Status Status { get; set; }
}
