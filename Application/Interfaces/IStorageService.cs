public interface IStorageService
{
    Task<string> SaveFileAsync(Stream fileStream, string fileName);
    Task<Stream> GetFileAsync(string filePath);
    Task DeleteFileAsync(string filePath);
}
