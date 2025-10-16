namespace Pakiza.Application.Features.DC.SqlScriptExecutionLog.Queries;

public record GetSqlScriptExecutionLogByIdQuery(Guid Id) : IQuery<GetSqlScriptExecutionLogByIdResult>;
public record GetSqlScriptExecutionLogByIdResult(SqlScriptExecutionLogDTO SqlScriptExecutionLog);
