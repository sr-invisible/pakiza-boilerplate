namespace Pakiza.Application.DTOs.User;

public class RoleDTO
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "Name is required.")] 
    public string Name { get; set; } = string.Empty;

    //public string? Status { get; set; } = "Active";
    //public bool IsActive { get; set; } = true;

}
