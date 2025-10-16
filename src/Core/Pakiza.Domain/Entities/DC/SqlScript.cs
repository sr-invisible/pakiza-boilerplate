using Pakiza.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pakiza.Domain.Entities.DC
{
    public class SqlScript : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
        public DateTime? ExecutedAt { get; set; }

        public ScriptStatus Status { get; set; }
        public ICollection<SqlScriptExecutionLog> ExecutionLogs { get; set; } = new List<SqlScriptExecutionLog>();
    }

}
