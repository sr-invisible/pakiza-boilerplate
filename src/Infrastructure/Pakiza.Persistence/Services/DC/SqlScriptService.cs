using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pakiza.Application.Features.DC.SQL.Response;
using Pakiza.Application.Helper.LocalStorageHelper;
using Pakiza.Application.Services;
using System.Text.RegularExpressions;
using System.Threading;
using static Dapper.SqlMapper;

namespace Pakiza.Persistence.Services.DC;

public class SqlScriptService : ISqlScriptService
{
    private readonly ISqlScriptRepository _iSqlScriptRepository;
    private readonly IFileStorageHelperService _iFileStorageHelperService;
    public SqlScriptService(ISqlScriptRepository iSqlScriptRepository, IFileStorageHelperService iFileStorageHelperService)
    {
        _iSqlScriptRepository = iSqlScriptRepository;
        _iFileStorageHelperService = iFileStorageHelperService;
    }

    public async Task<SqlScriptDTO> AddAsync(SqlScriptDTO model)
    {
        var fileName = $"{model.Name}_{model.Id}_V{model.Version}.sql";
        model.FilePath = await _iFileStorageHelperService.SaveAttachmentFileLocalAsync(model.ScriptFile!, fileName);
        var result = await _iSqlScriptRepository.AddAsync(model.Adapt<SqlScript>());
        await _iSqlScriptRepository.SaveChangesAsync();
        return result.Adapt<SqlScriptDTO>();
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var result = await _iSqlScriptRepository.RemoveAsync(id.ToString()); 
        await _iSqlScriptRepository.SaveChangesAsync();
        return result;
    }

    public Task<IReadOnlyList<SqlScriptDTO>> GetAllAsync()
    {
        return Task.FromResult(_iSqlScriptRepository.GetAll().Adapt<IReadOnlyList<SqlScriptDTO>>());
    }

    public async Task<SqlScriptDTO> GetByIdAsync(Guid id)
    {
        var result = (await _iSqlScriptRepository.GetByIdAsync(id.ToString())).Adapt<SqlScriptDTO>();
        var baseUrl = await _iFileStorageHelperService.GetAttachmentFileLocalDirectoryAsync();
        result.FilePath = string.IsNullOrEmpty(result.FilePath) ? result.FilePath : Path.Combine(baseUrl, result.FilePath);

        if (!string.IsNullOrWhiteSpace(result.FilePath) && File.Exists(result.FilePath))
            result.ScriptContent = await File.ReadAllTextAsync(result.FilePath);

        return result;
    }

    public async Task<SqlScriptDTO> UpdateAsync(SqlScriptDTO model)
    {
        var fileName = $"{model.Version}_{model.Id}.sql";
        var folderPath = await _iFileStorageHelperService.SaveAttachmentFileLocalAsync(model.ScriptFile!, model.Name);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var filePath = Path.Combine(folderPath, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await model.ScriptFile!.CopyToAsync(stream);
        }
        var result = await _iSqlScriptRepository.UpdateAsync(model.Adapt<SqlScript>());
        await _iSqlScriptRepository.SaveChangesAsync();

        return result.Adapt<SqlScriptDTO>();
    }
    public async Task<SqlScriptFileResponse> GetSqlScriptFileByIdAsync(Guid id)
    {
        var result = (await _iSqlScriptRepository.GetByIdAsync(id.ToString())).Adapt<SqlScriptDTO>();
        var baseUrl = await _iFileStorageHelperService.GetAttachmentFileLocalDirectoryAsync();
        result.FilePath = string.IsNullOrEmpty(result.FilePath) ? result.FilePath : Path.Combine(baseUrl, result.FilePath);

        if (result == null || !File.Exists(result.FilePath))
            throw new FileNotFoundException("SQL script file not found.");
        var sqlScriptFileResponse = new SqlScriptFileResponse
        {
            FileName = Path.GetFileName(result.FilePath),
            FileData = await File.ReadAllBytesAsync(result.FilePath)
        };
        return sqlScriptFileResponse;
    }

    private string Clean(string script)
    {
        if (string.IsNullOrWhiteSpace(script))
            return string.Empty;

        // Remove /* */ comments
        script = Regex.Replace(script, @"/\*.*?\*/", string.Empty, RegexOptions.Singleline);

        // Remove -- comments
        script = Regex.Replace(script, @"--.*?$", string.Empty, RegexOptions.Multiline);

        // Remove USE, GO, SET
        script = Regex.Replace(script, @"^\s*(USE|GO|SET)\b.*?$", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);

        // Clean whitespace
        var lines = script.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                          .Select(line => line.Trim())
                          .Where(line => !string.IsNullOrWhiteSpace(line));

        return string.Join(Environment.NewLine, lines);
    }
    private static string CleanSqlScript(string content)
    {
        var lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

        var cleanedLines = lines
            .Where(line =>
                !string.IsNullOrWhiteSpace(line) &&
                !line.TrimStart().StartsWith("USE", StringComparison.OrdinalIgnoreCase) &&
                !line.TrimStart().StartsWith("GO", StringComparison.OrdinalIgnoreCase) &&
                !line.TrimStart().StartsWith("SET ", StringComparison.OrdinalIgnoreCase) &&
                !line.TrimStart().StartsWith("/**") &&
                !line.TrimStart().StartsWith("--") &&
                !line.TrimStart().StartsWith("/*") &&
                !line.TrimStart().StartsWith("*") &&
                !line.TrimStart().StartsWith("*/")
            );

        return string.Join(Environment.NewLine, cleanedLines);
    }

}
