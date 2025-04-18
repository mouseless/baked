using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;

namespace Baked;

public static class RuntimeExtensions
{
    public static byte[] ToMD5(this string str)
    {
        using var md5 = MD5.Create();

        return md5.ComputeHash(str.ToUtf8Bytes());
    }

    public static byte[] ToSHA512(this string str)
    {
        using var sha512 = SHA512.Create();

        return sha512.ComputeHash(str.ToUtf8Bytes());
    }

    public static byte[] ToSHA384(this string str)
    {
        using var sha384 = SHA384.Create();

        return sha384.ComputeHash(str.ToUtf8Bytes());
    }

    public static byte[] ToSHA256(this string str)
    {
        using var sha256 = SHA256.Create();

        return sha256.ComputeHash(str.ToUtf8Bytes());
    }

    public static byte[] ToSHA1(this string str)
    {
        using var sha1 = SHA1.Create();

        return sha1.ComputeHash(str.ToUtf8Bytes());
    }

    public static string ToBase64(this byte[] bytes) =>
        Convert.ToBase64String(bytes);

    public static string ToBase64Url(this byte[] bytes) =>
        Base64Url.EncodeToString(bytes);

    public static byte[] FromBase64(this string @string) =>
        Convert.FromBase64String(@string);

    public static byte[] FromBase64Url(this string @string) =>
        Base64Url.DecodeFromChars(@string);

    public static byte[] ToUtf8Bytes(this string @string) =>
        Encoding.UTF8.GetBytes(@string);

    public static string ToUtf8String(this byte[] bytes) =>
        Encoding.UTF8.GetString(bytes);

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

    public static T GetRequiredValue<T>(this IConfiguration configuration, string key,
        T? defaultValue = default
    ) => (T)configuration.GetRequiredValue(typeof(T), key, defaultValue);

    public static object GetRequiredValue(this IConfiguration configuration, Type type, string key,
        object? defaultValue = default
    ) => configuration.GetValue(type, key, defaultValue) ??
           throw new InvalidOperationException($"Looked for a value {key} in Configurations, but could not found");
}