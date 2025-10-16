using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pakiza.Application.Request
{
    public class SqlScriptRequest
    {
        public string Name { get; set; } = string.Empty;
        public IFormFile? ScriptFile { get; set; } 
        public string? ScriptFilePath { get; set; }
        public string? Script { get; set; }
        public string Version { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
    }
}
