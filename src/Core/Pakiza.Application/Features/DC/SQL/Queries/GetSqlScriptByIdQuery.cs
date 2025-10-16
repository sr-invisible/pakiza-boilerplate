namespace Pakiza.Application.Features.DC.SQL.Queries;

public record GetSqlScriptByIdQuery(Guid Id) : IQuery<GetSqlScriptByIdResult>;
public record GetSqlScriptByIdResult(SqlScriptDTO SqlScript);
