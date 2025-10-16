
namespace Pakiza.Application.Repositories.Users;

public interface IRoleRepository : IRepository<Role>
{
    Task GetByNameAsync(string name); 
    Task<IEnumerable> GetRolesForUserAsync(int userId);
}
