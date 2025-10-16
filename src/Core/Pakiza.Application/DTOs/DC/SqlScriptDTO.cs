
namespace Pakiza.Application.DTOs.DC
{
    public class SqlScriptDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public IFormFile? ScriptFile { get; set; } 
        public string? FilePath { get; set; } 
        public string? Script { get; set; }
        public string Version { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
        public DateTime? ExecutedAt { get; set; }

        public string? ScriptContent { get; set; }
    }
}
