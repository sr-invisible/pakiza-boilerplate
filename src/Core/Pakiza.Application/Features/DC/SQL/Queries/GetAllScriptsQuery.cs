namespace Pakiza.Application.Features.DC.SQL.Queries;

public record GetSqlScriptsQuery() : IQuery<GetSqlScriptsResult>;
public record GetSqlScriptsResult(IReadOnlyList<SqlScriptDTO> Product);
