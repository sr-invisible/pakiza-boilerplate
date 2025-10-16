
namespace Pakiza.Persistence.Repositories.Users;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(IAppDbContext context) : base(context)
    {
    }
}
