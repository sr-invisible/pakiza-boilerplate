using Pakiza.Domain.Enums;

namespace Pakiza.Domain.Entities.DC;

[Table(name: "DC_CompanyProjectModules")]
public class DCCompanyProjectModules : BaseEntity
{
    [ForeignKey(nameof(DCCompanyProject))]
    public Guid RefCompanyProjectId { get; set; }

    [ForeignKey(nameof(DCModule))]
    public Guid RefModuleId { get; set; }

    public Status Status { get; set; }

    public DCCompanyProject DCCompanyProject { get; set; } = default!;
    public DCModule DCModule { get; set; } = default!;

}
