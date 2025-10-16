namespace Pakiza.Application.DTOs.DC;

public class DCCompanyInfoDTO :  BaseDTO
{
    [Display(Name = "Company Name")]
    public string CompanyName { get; set; } = string.Empty;

    public string? Address { get; set; }

    [Display(Name = "Contact Person")]
    public string? ContactPerson { get; set; }

    public string Phone { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? WebUrl { get; set; }
}
