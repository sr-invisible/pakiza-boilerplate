
namespace Pakiza.Domain.Entities.Users;

public class User : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime? LastLogin { get; private set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public ICollection<UserToken> Tokens { get; set; } = new List<UserToken>();
}
