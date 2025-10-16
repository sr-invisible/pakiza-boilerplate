using Pakiza.Domain.Enums;
namespace Pakiza.Application.Features.DC.SqlScriptExecutionLog.Request;

public class SqlScriptExecutionLogRequest
{
    public Guid ScriptId { get; set; }
    public DateTime ExecutedAt { get; set; }
    public ScriptStatus Status { get; set; }
    public string Message { get; set; } = string.Empty;
}
