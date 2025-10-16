
namespace Pakiza.Persistence.Repositories.DC;

public class SqlScriptRepository : Repository<SqlScript>, ISqlScriptRepository
{
    public SqlScriptRepository(IAppDbContext context) : base(context)
    {
    }
}
