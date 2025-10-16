
namespace Pakiza.Persistence.Repositories.DC;

public class SqlScriptExecutionLogRepository : Repository<SqlScriptExecutionLog>, ISqlScriptExecutionLogRepository
{
    public SqlScriptExecutionLogRepository(IAppDbContext context) : base(context)
    {
    }
}
