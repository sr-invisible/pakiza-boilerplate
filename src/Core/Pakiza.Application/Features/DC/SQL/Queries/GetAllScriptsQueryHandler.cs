
namespace Pakiza.Application.Features.DC.SQL.Queries;

public class GetSqlScriptsQueryHandler(ISqlScriptService sqlScriptService)
: IQueryHandler<GetSqlScriptsQuery, GetSqlScriptsResult>
{
    public async Task<GetSqlScriptsResult> Handle(GetSqlScriptsQuery query, CancellationToken cancellationToken)
    {
        var result = await sqlScriptService.GetAllAsync();
        return new GetSqlScriptsResult(result);
    }
}
