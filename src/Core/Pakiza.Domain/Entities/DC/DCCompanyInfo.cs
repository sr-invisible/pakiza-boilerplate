using Pakiza.Domain.Enums;

namespace Pakiza.Domain.Entities.DC;

[Table(name: "DC_CompanyInfo")]
public class DCCompanyInfo : BaseEntity
{
    [Required, MaxLength(200)]
    public string CompanyName { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Address { get; set; }

    [MaxLength(200)]
    public string? ContactPerson { get; set; }

    [Required, MaxLength(50)] 
    public string Phone { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Email { get; set; }

    [MaxLength(50)]
    public string? WebUrl { get; set; }

    public Status Status { get; set; }

    [NotMapped]
    public ICollection<DCCompanyProject> DCCompanyProjects { get; set; } = new List<DCCompanyProject>();
}
