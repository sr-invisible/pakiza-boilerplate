namespace Pakiza.Application.DTOs.User;

public class PermissionDTO
{
    [Required(ErrorMessage = "Role is required")]
    public string RoleId { get; set; } = string.Empty;

    //[NonZero(ErrorMessage = "Menu is required. Please select a valid menu")]
    [Display(Name = "Menu")]
    public int MenuId { get; set; }

    [Required(ErrorMessage = "Permission type is required")]
    [RegularExpression("^(Full|Read)$", ErrorMessage = "PermissionType must be 'Full' or 'Read'.")]
    public string PermissionType { get; set; } = string.Empty;
}
