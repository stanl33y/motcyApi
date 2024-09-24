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

    public async Task<string> SaveFileFromBase64Async(string base64String, string fileName)
    {
        byte[] bytes = Convert.FromBase64String(base64String);
        var filePath = Path.Combine(_storageBasePath, fileName);
        var uniqueFileName = GetUniqueFileName(filePath);

        await File.WriteAllBytesAsync(uniqueFileName, bytes);

        return uniqueFileName;
    }

    public Stream GetFile(string filePath)
    {
        if (!File.Exists($"{_storageBasePath}/{filePath}"))
        {
            throw new FileNotFoundException("The requested file was not found.", $"{_storageBasePath}/{filePath}");
        }

        return new FileStream($"{_storageBasePath}/{filePath}", FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
    }

    public async Task<string> GetFileAsBase64Async(string filePath)
    {
        if (!File.Exists($"{_storageBasePath}/{filePath}"))
        {
            return "";
        }

        byte[] bytes = await File.ReadAllBytesAsync($"{_storageBasePath}/{filePath}");
        return Convert.ToBase64String(bytes);
    }

    public Task DeleteFileAsync(string filePath)
    {
        if (File.Exists($"{_storageBasePath}/{filePath}"))
        {
            File.Delete($"{_storageBasePath}/{filePath}");
        }
        return Task.CompletedTask;
    }

    private string GetUniqueFileName(string filePath)
    {
        string directory = Path.GetDirectoryName($"{_storageBasePath}/{filePath}") ?? _storageBasePath;
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension($"{_storageBasePath}/{filePath}");
        string extension = Path.GetExtension($"{_storageBasePath}/{filePath}");
        int count = 1;

        while (File.Exists(filePath))
        {
            string tempFileName = $"{fileNameWithoutExtension}_{count++}";
            filePath = Path.Combine(directory, tempFileName + extension);
        }

        return filePath;
    }
}
