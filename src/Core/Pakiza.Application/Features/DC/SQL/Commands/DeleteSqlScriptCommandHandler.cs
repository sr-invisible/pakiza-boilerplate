namespace Pakiza.Application.Features.DC.SQL.Commands;

public class DeleteSqlScriptHandler(ISqlScriptService sqlScriptService)
    : ICommandHandler<DeleteSqlScriptCommand, DeleteSqlScriptResult>
{
    public async Task<DeleteSqlScriptResult> Handle(DeleteSqlScriptCommand cmd, CancellationToken cancellationToken)
    {
        await sqlScriptService.DeleteByIdAsync(cmd.Id);
        return new DeleteSqlScriptResult(true);
    }
}
