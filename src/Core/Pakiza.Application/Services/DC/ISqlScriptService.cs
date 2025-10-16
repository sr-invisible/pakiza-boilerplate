using Pakiza.Application.Features.DC.SQL.Response;

namespace Pakiza.Application.Services.DC;

public interface ISqlScriptService : IService<SqlScriptDTO>
{
    Task<SqlScriptFileResponse> GetSqlScriptFileByIdAsync(Guid id);
}
