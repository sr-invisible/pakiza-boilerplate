using Microsoft.EntityFrameworkCore;
using Pakiza.Application.DTOs.Products;
using Pakiza.Application.Helper.LocalStorageHelper;
using Pakiza.Application.Services;
using System.Threading;

namespace Pakiza.Persistence.Services.DC;

public class SqlScriptExecutionLogService : ISqlScriptExecutionLogService
{
    private readonly ISqlScriptExecutionLogRepository _iSqlScriptExecutionLogRepository;
    public SqlScriptExecutionLogService(ISqlScriptExecutionLogRepository iSqlScriptExecutionLogRepository)
    {
        _iSqlScriptExecutionLogRepository = iSqlScriptExecutionLogRepository;
    }

    public async Task<SqlScriptExecutionLogDTO> AddAsync(SqlScriptExecutionLogDTO model)
    {
        var result = await _iSqlScriptExecutionLogRepository.AddAsync(model.Adapt<SqlScriptExecutionLog>());
        await _iSqlScriptExecutionLogRepository.SaveChangesAsync();
        return result.Adapt<SqlScriptExecutionLogDTO>();
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<SqlScriptExecutionLogDTO>> GetAllAsync()
    {
        var result = await Task.FromResult(_iSqlScriptExecutionLogRepository.GetAll(false, true));
        return result.Adapt<IReadOnlyList<SqlScriptExecutionLogDTO>>();
    }

    public async Task<SqlScriptExecutionLogDTO> GetByIdAsync(Guid id)
    {
        var result = await _iSqlScriptExecutionLogRepository.GetByIdAsync(id.ToString());
        return result.Adapt<SqlScriptExecutionLogDTO>();
    }

    public Task<SqlScriptExecutionLogDTO> UpdateAsync(SqlScriptExecutionLogDTO model)
    {
        throw new NotImplementedException();
    }
}
