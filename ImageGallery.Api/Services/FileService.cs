using ImageGallery.Api.Interfaces.Services;

namespace ImageGallery.Api.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void DeleteFile(string path)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(path);

            var newPath = _environment.WebRootPath + path;

            if (File.Exists(newPath))
                File.Delete(newPath);
        }

        public async Task<string> UploadFileAsync(string directory, IFormFile file)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(directory);

            ArgumentNullException.ThrowIfNull(file);

            // Create a random file name, then create a path to the directory within the root directory
            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var path = Path.Combine(_environment.WebRootPath, $"{directory}/", fileName);

            // Create the new file at the path specified above
            await using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/{directory}/" + fileName;
        }
    }
}
