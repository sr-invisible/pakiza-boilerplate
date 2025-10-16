namespace Pakiza.Application.DTOs.DC;

public class DCCompanyInfoUpsertRequest
{
    public Guid? Id { get; set; }
    [Display(Name = "Company Name")]
    public string CompanyName { get; set; } = string.Empty;

    public string? Address { get; set; }

    [Display(Name = "Contact Person")]
    public string? ContactPerson { get; set; }

    public string Phone { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? WebUrl { get; set; }

    //common props
    public string? Status { get; set; }
    public bool? IsActive { get; set; } = true;
}
