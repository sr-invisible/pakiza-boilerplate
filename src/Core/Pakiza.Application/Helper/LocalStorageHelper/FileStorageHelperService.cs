using Microsoft.AspNetCore.Hosting;

namespace Pakiza.Application.Helper.LocalStorageHelper;

public class FileStorageHelperService : IFileStorageHelperService
{
    private readonly string _storagePath;
    private readonly IHttpContextAccessor _iHttpContextAccessor;
    private readonly IWebHostEnvironment _iHostingEnvironment;

    public FileStorageHelperService(IHttpContextAccessor iHttpContextAccessor, IWebHostEnvironment iHostingEnvironment)
    {
        _iHttpContextAccessor = iHttpContextAccessor;
        _iHostingEnvironment = iHostingEnvironment;
        _storagePath = Path.Combine(_iHostingEnvironment.WebRootPath, "uploads");
    }

    public FileStorageHelperService(IHttpContextAccessor iHttpContextAccessor, IWebHostEnvironment iHostingEnvironment, string storagePath)
    {
        _iHttpContextAccessor = iHttpContextAccessor;
        _iHostingEnvironment = iHostingEnvironment;
        _storagePath = storagePath;
    }

    public async Task<string> SaveImageLocalAsync(IFormFile file, string imageName)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file.");
        
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (string.IsNullOrEmpty(fileExtension))
            throw new ArgumentException("Invalid file extension.");

        var controllerName = _iHttpContextAccessor.HttpContext?.Request.RouteValues["controller"]?.ToString()?.ToLower();
        var directory = Path.Combine(_storagePath, "images", controllerName!);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        var fileName = imageName + fileExtension;
        var filePath = Path.Combine(directory, fileName);

        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to save the image.", ex);
        }
    }

    public Task DeleteImageLocalAsync(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            //throw new ArgumentException("Invalid file name.");
            return Task.CompletedTask;
        }
        var controllerName = _iHttpContextAccessor.HttpContext?.Request.RouteValues["controller"]?.ToString()?.ToLower();
        var directory = Path.Combine(_storagePath, "images", controllerName!);
        var filePath = Path.Combine(directory, fileName);

        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            // Handle exception, log error, etc.
            throw new Exception("Failed to delete the image.", ex);
        }
    }

    public async Task<string> SaveAttachmentFileLocalAsync(IFormFile file,string attachmentName)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("Invalid file.");
        }

        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (string.IsNullOrEmpty(fileExtension))
        {
            throw new ArgumentException("Invalid file extension.");
        }

        var controllerName = _iHttpContextAccessor.HttpContext?.Request.RouteValues["controller"]?.ToString()?.ToLower();
        var directory = Path.Combine(_storagePath, "docs", controllerName!);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        var fileName = attachmentName + fileExtension;
        var filePath = Path.Combine(directory, fileName);

        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
        catch (Exception ex)
        {
            // Handle exception, log error, etc.
            throw new Exception("Failed to save the image.", ex);
        }
    }

    public Task DeleteAttachmentFileLocalAsync(string attachmentName)
    {
        if (string.IsNullOrWhiteSpace(attachmentName))
        {
            throw new ArgumentException("Invalid file name.");
        }
        var controllerName = _iHttpContextAccessor.HttpContext?.Request.RouteValues["controller"]?.ToString()?.ToLower();
        var directory = Path.Combine(_storagePath, "docs", controllerName!);
        var filePath = Path.Combine(directory, attachmentName);

        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            // Handle exception, log error, etc.
            throw new Exception("Failed to delete the image.", ex);
        }
    }

    public async Task<string> GetImageLocalDirectoryAsync(string controller = null)
    { 
        var request = _iHttpContextAccessor.HttpContext?.Request;
        var controllerName = controller?? request?.RouteValues["controller"]?.ToString()?.ToLower();
        var returnDirectory = request?.Scheme + "://" + request?.Host +"/uploads/images/" + controllerName+"/";
        
        //var directory = Path.Combine(_storagePath, "images", controllerName!);
        //bool directoryExists = Directory.Exists(directory);

        //if (!directoryExists)
        //    throw new DirectoryNotFoundException($"Directory '{directory}' does not exist.");
        return await Task.FromResult(returnDirectory);
    }

    public async Task<string> GetImageLocalDirectoryAsync()
    { 
        var request = _iHttpContextAccessor.HttpContext?.Request;
        var controllerName =  request?.RouteValues["controller"]?.ToString()?.ToLower();
        var returnDirectory = request?.Scheme + "://" + request?.Host +"/uploads/images/" + controllerName+"/";
        
        //var directory = Path.Combine(_storagePath, "images", controllerName!);
        //bool directoryExists = Directory.Exists(directory);

        //if (!directoryExists)
        //    throw new DirectoryNotFoundException($"Directory '{directory}' does not exist.");
        return await Task.FromResult(returnDirectory);
    }

    public async Task<string> GetAttachmentFileLocalDirectoryAsync()
    {
        var controllerName = _iHttpContextAccessor.HttpContext?.Request.RouteValues["controller"]?.ToString()?.ToLower();
        var returnDirectory = "/uploads/docs/";
        var directory = Path.Combine(_storagePath, "docs", controllerName!);
        bool directoryExists = Directory.Exists(directory);

        //if (!directoryExists)
        //    throw new DirectoryNotFoundException($"Directory '{directory}' does not exist.");
        return await Task.FromResult(directory);
    }
    public async Task<string> GetImageLocalDirectoryWithControllerAsync()
    {
        var controllerName = _iHttpContextAccessor.HttpContext?.Request.RouteValues["controller"]?.ToString()?.ToLower();
        var returnDirectory = "/uploads/images/" + controllerName + "/";
        var directory = Path.Combine(_storagePath, "images", controllerName!);
        bool directoryExists = Directory.Exists(directory);

        //if (!directoryExists)
        //    throw new DirectoryNotFoundException($"Directory '{directory}' does not exist.");
        return await Task.FromResult(returnDirectory);
    }

    public async Task<string> GetAttachmentFileLocalDirectoryWithControllerAsync()
    {
        var controllerName = _iHttpContextAccessor.HttpContext?.Request.RouteValues["controller"]?.ToString()?.ToLower();
        var returnDirectory = "/uploads/docs/" + controllerName + "/";
        var directory = Path.Combine(_storagePath, "docs", controllerName!);
        bool directoryExists = Directory.Exists(directory);

        //if (!directoryExists)
        //    throw new DirectoryNotFoundException($"Directory '{directory}' does not exist.");
        return await Task.FromResult(returnDirectory);
    }
}
