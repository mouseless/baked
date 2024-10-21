using System.Text;

namespace Microsoft.Extensions.FileProviders;

public static class FileProviderExtensions
{
    public static string? ReadAsString(this IFileProvider provider, string subPath)
    {
        using var stream = provider.CreateReadStream(subPath);
        using var streamReader = new StreamReader(stream, Encoding.UTF8);

        return streamReader.ReadToEnd();
    }

    public static async Task<string?> ReadAsStringAsync(this IFileProvider provider, string subPath)
    {
        using var stream = provider.CreateReadStream(subPath);
        using var streamReader = new StreamReader(stream, Encoding.UTF8);

        return await streamReader.ReadToEndAsync();
    }

    static Stream CreateReadStream(this IFileProvider provider, string subPath)
    {
        var fileInfo = provider.GetFileInfo(subPath);
        if (!fileInfo.Exists) { return new MemoryStream(); }

        return fileInfo.CreateReadStream();
    }
}