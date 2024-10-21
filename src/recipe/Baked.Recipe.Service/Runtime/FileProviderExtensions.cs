using System.Text;

namespace Microsoft.Extensions.FileProviders;

public static class FileProviderExtensions
{
    public static string? ReadAsString(this IFileProvider provider, string subPath)
    {
        using var streamReader = provider.CreateReadStream(subPath);

        return streamReader.ReadToEnd();
    }

    public static async Task<string?> ReadAsStringAsync(this IFileProvider provider, string subPath)
    {
        using var streamReader = provider.CreateReadStream(subPath);

        return await streamReader.ReadToEndAsync();
    }

    static StreamReader CreateReadStream(this IFileProvider provider, string subPath)
    {
        var fileInfo = provider.GetFileInfo(subPath);
        if (!fileInfo.Exists) { return StreamReader.Null; }

        return new StreamReader(fileInfo.CreateReadStream(), Encoding.UTF8);
    }
}