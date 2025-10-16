namespace Pakiza.Application.Features.DC.SqlScriptExecutionLog.Queries;

public class GetSqlScriptExecutionLogsQueryHandler(ISqlScriptExecutionLogService SqlScriptExecutionLogService)
: IQueryHandler<GetSqlScriptExecutionLogsQuery, GetSqlScriptExecutionLogsResult>
{
    public async Task<GetSqlScriptExecutionLogsResult> Handle(GetSqlScriptExecutionLogsQuery query, CancellationToken cancellationToken)
    {
        var result = await SqlScriptExecutionLogService.GetAllAsync();
        return new GetSqlScriptExecutionLogsResult(result);
    }
}
