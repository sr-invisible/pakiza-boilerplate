using Pakiza.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pakiza.Domain.Entities.DC
{
    public class SqlScriptExecutionLog : BaseEntity
    {
        public Guid ScriptId { get; set; }
        public DateTime ExecutedAt { get; set; }
        public ScriptStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;

        public SqlScript Script { get; set; } = default!;
    }

}
