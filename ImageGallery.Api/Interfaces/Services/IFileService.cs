namespace ImageGallery.Api.Interfaces.Services
{
    public interface IFileService
    {
        void DeleteFile(string path);
        Task<string> UploadFileAsync(string directory, IFormFile file);
    }
}
