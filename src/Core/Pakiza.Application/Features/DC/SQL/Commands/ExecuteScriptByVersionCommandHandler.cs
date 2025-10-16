namespace Pakiza.Application.Features.DC.SQL.Commands;

public class ExecuteScriptByVersionCommandHandler(ISqlScriptService sqlScriptService)
: ICommandHandler<ExecuteScriptByVersionCommand, ExecuteScriptByVersionResult>
{
    public async Task<ExecuteScriptByVersionResult> Handle(ExecuteScriptByVersionCommand cmd, CancellationToken cancellationToken)
    {
        cmd.SqlScript.Id = Guid.NewGuid();
        var result = await sqlScriptService.AddAsync(cmd.SqlScript);
        return new ExecuteScriptByVersionResult(result.Id);
    }
}
