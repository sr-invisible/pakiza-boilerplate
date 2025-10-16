using Pakiza.Domain.Enums;

namespace Pakiza.Domain.Entities.DC;

[Table(name: "DC_CompanyProject")]
public class DCCompanyProject : BaseEntity
{
    [ForeignKey(nameof(DCCompanyInfo))]
    public Guid RefCompanyInfoId { get; set; }

    [ForeignKey(nameof(DCProject))]
    public Guid RefProjectId { get; set; }

    [MaxLength(50)]
    public string? CompanyProjectCode { get; set; }

    public DateTime ValidDate { get; set; }

    [MaxLength(100)]
    public string? MasterPassword { get; set; }

    [MaxLength(200)]
    public string? ProjectKey { get; set; }
    public Status Status { get; set; }

    public DCCompanyInfo DCCompanyInfo { get; set; } = default!;
    public DCProject DCProject { get; set; } = default!;
    public ICollection<DCCompanyProjectModules> DCCompanyProjectModules { get; set; } = default!;
}
