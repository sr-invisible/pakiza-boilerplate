namespace Pakiza.Application.Features.DC.SqlScriptExecutionLog.Commands;

public class CreateSqlScriptExecutionLogCommandHandler(ISqlScriptExecutionLogService SqlScriptExecutionLogService)
: ICommandHandler<CreateSqlScriptExecutionLogCommand, CreateSqlScriptExecutionLogResult>
{
    public async Task<CreateSqlScriptExecutionLogResult> Handle(CreateSqlScriptExecutionLogCommand cmd, CancellationToken cancellationToken)
    {
        cmd.SqlScriptExecutionLog.Id = Guid.NewGuid();
        var result = await SqlScriptExecutionLogService.AddAsync(cmd.SqlScriptExecutionLog);
        return new CreateSqlScriptExecutionLogResult(result.Id);
    }
}
