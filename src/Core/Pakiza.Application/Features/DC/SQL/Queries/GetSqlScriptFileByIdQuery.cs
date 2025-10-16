using Pakiza.Application.Features.DC.SQL.Response;

namespace Pakiza.Application.Features.DC.SQL.Queries;

public record GetSqlScriptFileByIdQuery(Guid Id) : IQuery<GetSqlScriptFileByIdQueryResult>;
public record GetSqlScriptFileByIdQueryResult(SqlScriptFileResponse SqlScriptFileResponse);
