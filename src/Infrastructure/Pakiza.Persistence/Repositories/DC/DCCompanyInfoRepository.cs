

namespace Pakiza.Persistence.Repositories.DC;

public class DCCompanyInfoRepository : DapperRepository<DCCompanyInfo>, IDCCompanyInfoRepository
{
    public DCCompanyInfoRepository(IDbConnection dbConnection, AppDbContext context) : base(dbConnection, context)
    {
        
    }
}
