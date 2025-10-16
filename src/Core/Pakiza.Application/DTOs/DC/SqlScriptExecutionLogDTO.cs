using Pakiza.Domain.Entities.DC;
using Pakiza.Domain.Enums;
namespace Pakiza.Application.DTOs.DC;

public class SqlScriptExecutionLogDTO : BaseEntity
{
    public Guid ScriptId { get; set; }
    public DateTime ExecutedAt { get; set; }
    public ScriptStatus Status { get; set; }
    public string Message { get; set; } = string.Empty;

    public SqlScript Script { get; set; } = default!;
}
