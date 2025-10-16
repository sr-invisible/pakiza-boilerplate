namespace Pakiza.Application.DTOs.User;

public class UserTokenDTO : BaseDTO
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string RefreshToken { get; set; } = default!;

    [Required]
    public new DateTime DateCreated { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime DateExpired { get; set; }

    [Required]
    public int LifeTime { get; set; } = 2;

    public UserTokenDTO()
    {
        DateExpired = DateCreated.AddMinutes(LifeTime);
    }
}
