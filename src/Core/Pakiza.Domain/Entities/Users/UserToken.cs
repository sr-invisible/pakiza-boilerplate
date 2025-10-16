using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pakiza.Domain.Entities.Users;

public class UserToken : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string RefreshToken { get; set; } = default!;

    [Required]
    public DateTime DateExpired { get; set; }

    [Required]
    public int LifeTime { get; set; } = 2;

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = default!;

    public UserToken()
    {
        DateExpired = DateCreated.AddMinutes(LifeTime);
    }
}
