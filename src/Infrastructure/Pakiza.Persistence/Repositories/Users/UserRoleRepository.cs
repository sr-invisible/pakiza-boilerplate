
namespace Pakiza.Persistence.Repositories.Users;

public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(IAppDbContext context) : base(context)
    {
    }

}
