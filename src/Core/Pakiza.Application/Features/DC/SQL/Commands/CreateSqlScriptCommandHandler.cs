namespace Pakiza.Application.Features.DC.SQL.Commands;

public class CreateSqlScriptCommandHandler(ISqlScriptService sqlScriptService)
: ICommandHandler<CreateSqlScriptCommand, CreateSqlScriptResult>
{
    public async Task<CreateSqlScriptResult> Handle(CreateSqlScriptCommand cmd, CancellationToken cancellationToken)
    {
        cmd.SqlScript.Id = Guid.NewGuid();
        var result = await sqlScriptService.AddAsync(cmd.SqlScript);
        return new CreateSqlScriptResult(result.Id);
    }
}
