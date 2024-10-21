using System.Text;

namespace Microsoft.Extensions.FileProviders;

public static class FileProviderExtensions
{
    public static string? ReadAsString(this IFileProvider provider, string subPath)
    {
        using var streamReader = provider.CreateStreamReader(subPath);

        return streamReader.ReadToEnd();
    }

    public static async Task<string?> ReadAsStringAsync(this IFileProvider provider, string subPath)
    {
        using var streamReader = provider.CreateStreamReader(subPath);

        return await streamReader.ReadToEndAsync();
    }

    static StreamReader CreateStreamReader(this IFileProvider provider, string subPath)
    {
        var fileInfo = provider.GetFileInfo(subPath);
        if (!fileInfo.Exists) { return StreamReader.Null; }

        return new(fileInfo.CreateReadStream(), Encoding.UTF8);
    }
}