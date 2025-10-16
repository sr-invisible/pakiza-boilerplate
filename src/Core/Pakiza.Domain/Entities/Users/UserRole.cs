
namespace Pakiza.Domain.Entities.Users;

public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = default!;

    [ForeignKey(nameof(RoleId))]
    public Role Role { get; set; } = default!;
}
