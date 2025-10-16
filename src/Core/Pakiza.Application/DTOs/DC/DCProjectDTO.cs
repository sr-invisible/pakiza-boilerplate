namespace Pakiza.Application.DTOs.DC;

public class DCProjectDTO : BaseDTO
{
    [Display(Name = "Project Name")]
    public string ProjectName { get; set; } = string.Empty;
}