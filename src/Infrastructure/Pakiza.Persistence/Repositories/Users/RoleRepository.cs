
namespace Pakiza.Persistence.Repositories.Users;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(IAppDbContext context) : base(context)
    {
    }

    public Task GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable> GetRolesForUserAsync(int userId)
    {
        throw new NotImplementedException();
    }
}
