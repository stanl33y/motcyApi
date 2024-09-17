public class LocalFileStorageService : IStorageService
{
    private readonly string _storageBasePath;
    private readonly IConfiguration _configuration;

    public LocalFileStorageService(IConfiguration configuration)
    {
        _configuration = configuration;

        _storageBasePath = _configuration["Storage:LocalBasePath"]
            ?? Path.Combine(Directory.GetCurrentDirectory(), "Storage");

        if (!Directory.Exists(_storageBasePath))
        {
            Directory.CreateDirectory(_storageBasePath);
        }
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName)
    {
        var filePath = Path.Combine(_storageBasePath, fileName);
        var uniqueFileName = GetUniqueFileName(filePath);

        using (var file = File.Create(uniqueFileName))
        {
            await fileStream.CopyToAsync(file);
        }

        return uniqueFileName;
    }

    public async Task<Stream> GetFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("The requested file was not found.", filePath);
        }

        return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
    }

    public Task DeleteFileAsync(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        return Task.CompletedTask;
    }

    private string GetUniqueFileName(string filePath)
    {
        string directory = Path.GetDirectoryName(filePath);
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
        string extension = Path.GetExtension(filePath);
        int count = 1;

        while (File.Exists(filePath))
        {
            string tempFileName = $"{fileNameWithoutExtension}_{count++}";
            filePath = Path.Combine(directory, tempFileName + extension);
        }

        return filePath;
    }
}
