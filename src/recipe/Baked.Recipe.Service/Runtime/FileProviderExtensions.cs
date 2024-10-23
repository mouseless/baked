using System.Text;

namespace Microsoft.Extensions.FileProviders;

public static class FileProviderExtensions
{
    public static bool Exists(this IFileProvider provider, string subpath) =>
        provider.GetFileInfo(subpath).Exists;

    public static string? ReadAsString(this IFileProvider provider, string subpath)
    {
        using var streamReader = provider.CreateStreamReader(subpath);

        return streamReader.ReadToEnd();
    }

    public static async Task<string?> ReadAsStringAsync(this IFileProvider provider, string subpath)
    {
        using var streamReader = provider.CreateStreamReader(subpath);

        return await streamReader.ReadToEndAsync();
    }

    static StreamReader CreateStreamReader(this IFileProvider provider, string subpath)
    {
        var fileInfo = provider.GetFileInfo(subpath);
        if (!fileInfo.Exists) { return StreamReader.Null; }

        return new(fileInfo.CreateReadStream(), Encoding.UTF8);
    }
}