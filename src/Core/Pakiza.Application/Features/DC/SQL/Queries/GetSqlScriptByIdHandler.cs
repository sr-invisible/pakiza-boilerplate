namespace Pakiza.Application.Features.DC.SQL.Queries;

public class GetSqlScriptByIdHandler(ISqlScriptService sqlScriptService)
    : IQueryHandler<GetSqlScriptByIdQuery, GetSqlScriptByIdResult>
{
    public async Task<GetSqlScriptByIdResult> Handle(GetSqlScriptByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await sqlScriptService.GetByIdAsync(query.Id);
        return new GetSqlScriptByIdResult(result);
    }
}
