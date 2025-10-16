using Pakiza.Domain.Enums;

namespace Pakiza.Domain.Entities.DC;

[Table(name: "DC_Module")]
public class DCModule : BaseEntity
{
    [Required, MaxLength(200)]
    public string ModuleName { get; set; } = string.Empty;

    public Status Status { get; set; }

}
