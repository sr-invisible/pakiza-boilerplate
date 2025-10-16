
namespace Pakiza.Application.Helper.LocalStorageHelper;

public interface IFileStorageHelperService
{
    Task<string> SaveImageLocalAsync(IFormFile file, string imageName);
    Task<string> GetImageLocalDirectoryAsync();
    Task<string> GetImageLocalDirectoryAsync(string controller);
    Task DeleteImageLocalAsync(string fileName);
    Task<string> SaveAttachmentFileLocalAsync(IFormFile file, string attachmentName);
    Task<string> GetAttachmentFileLocalDirectoryAsync();
    Task DeleteAttachmentFileLocalAsync(string attachmentName);
    Task<string> GetImageLocalDirectoryWithControllerAsync();
    Task<string> GetAttachmentFileLocalDirectoryWithControllerAsync();
}

