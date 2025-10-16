namespace Pakiza.Persistence.Repositories.DC;

public class DCProjectRepository : DapperRepository<DCProject>, IDCProjectRepository
{
    public DCProjectRepository(IDbConnection dbConnection, AppDbContext context) : base(dbConnection, context)
    {

    }
}