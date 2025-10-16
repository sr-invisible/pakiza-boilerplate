
namespace Pakiza.Persistence.Repositories.Users;

public class UsersTokenRepository : Repository<UserToken>, IUsersTokenRepository
{
    public UsersTokenRepository(AppDbContext context) : base(context)
    {
    }
}
