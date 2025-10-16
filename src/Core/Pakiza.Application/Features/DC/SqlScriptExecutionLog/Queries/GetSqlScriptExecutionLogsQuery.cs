namespace Pakiza.Application.Features.DC.SqlScriptExecutionLog.Queries;

public record GetSqlScriptExecutionLogsQuery() : IQuery<GetSqlScriptExecutionLogsResult>;
public record GetSqlScriptExecutionLogsResult(IReadOnlyList<SqlScriptExecutionLogDTO> SqlScriptExecutionLogs);
