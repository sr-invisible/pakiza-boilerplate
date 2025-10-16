namespace Pakiza.Application.DTOs.DC;

public class DCProjectUpsertRequest
{
    public Guid? Id { get; set; }
    [Display(Name = "Project Name")]
    public string ProjectName { get; set; } = string.Empty;

    //common props
    public string? Status { get; set; }
    public bool? IsActive { get; set; } = true;
}