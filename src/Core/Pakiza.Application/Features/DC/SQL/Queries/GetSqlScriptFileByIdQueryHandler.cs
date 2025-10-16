namespace Pakiza.Application.Features.DC.SQL.Queries;

public class GetSqlScriptFileByIdQueryHandler(ISqlScriptService sqlScriptService)
    : IQueryHandler<GetSqlScriptFileByIdQuery, GetSqlScriptFileByIdQueryResult>
{
    public async Task<GetSqlScriptFileByIdQueryResult> Handle(GetSqlScriptFileByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await sqlScriptService.GetSqlScriptFileByIdAsync(query.Id);
        return new GetSqlScriptFileByIdQueryResult(result);
    }
}
