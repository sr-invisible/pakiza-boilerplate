namespace Pakiza.Application.Features.DC.SqlScriptExecutionLog.Queries;

public class GetSqlScriptExecutionLogByIdHandler(ISqlScriptExecutionLogService SqlScriptExecutionLogService)
    : IQueryHandler<GetSqlScriptExecutionLogByIdQuery, GetSqlScriptExecutionLogByIdResult>
{
    public async Task<GetSqlScriptExecutionLogByIdResult> Handle(GetSqlScriptExecutionLogByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await SqlScriptExecutionLogService.GetByIdAsync(query.Id);
        return new GetSqlScriptExecutionLogByIdResult(result);
    }
}
