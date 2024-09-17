public interface IStorageService
{
    Task<string> SaveFileAsync(Stream fileStream, string fileName);
    Task<string> SaveFileFromBase64Async(string base64String, string fileName);
    Task<Stream> GetFileAsync(string filePath);
    Task<string> GetFileAsBase64Async(string filePath);
    Task DeleteFileAsync(string filePath);
}
