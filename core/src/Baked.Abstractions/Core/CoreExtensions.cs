using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;

namespace Baked;

public static class CoreExtensions
{
    extension(TimeProvider timeProvider)
    {
        public DateTime GetNow() =>
            timeProvider.GetLocalNow().DateTime;
    }

    extension(IConfiguration configuration)
    {
        public T GetRequiredValue<T>(string key,
            T? defaultValue = default
        ) => (T)configuration.GetRequiredValue(typeof(T), key, defaultValue);

        public object GetRequiredValue(Type type, string key,
            object? defaultValue = default
        ) => configuration.GetValue(type, key, defaultValue) ??
            throw new InvalidOperationException($"Looked for a value {key} in Configurations, but could not found");
    }

    extension<T>(IEnumerable<T> enumerable)
    {
        public string Join(char separator) =>
            enumerable.Join($"{separator}");

        public string Join(
            string? separator = default
        ) => string.Join(separator ?? string.Empty, enumerable);
    }

    extension(IFileProvider provider)
    {
        public bool Exists(string subpath) =>
            provider.GetFileInfo(subpath).Exists;

        public string? ReadAsString(string subpath)
        {
            using var streamReader = provider.CreateStreamReader(subpath);

            return streamReader.ReadToEnd();
        }

        public async Task<string?> ReadAsStringAsync(string subpath)
        {
            using var streamReader = provider.CreateStreamReader(subpath);

            return await streamReader.ReadToEndAsync();
        }

        StreamReader CreateStreamReader(string subpath)
        {
            var fileInfo = provider.GetFileInfo(subpath);
            if (!fileInfo.Exists) { return StreamReader.Null; }

            return new(fileInfo.CreateReadStream(), Encoding.UTF8);
        }
    }

    extension(string str)
    {
        #region Encryption

        public byte[] ToMD5()
        {
            using var md5 = MD5.Create();
            // TODO - öneriye bir bak
            return md5.ComputeHash(str.ToUtf8Bytes());
        }

        public byte[] ToSHA512()
        {
            using var sha512 = SHA512.Create();

            return sha512.ComputeHash(str.ToUtf8Bytes());
        }

        public byte[] ToSHA384()
        {
            using var sha384 = SHA384.Create();

            return sha384.ComputeHash(str.ToUtf8Bytes());
        }

        public byte[] ToSHA256()
        {
            using var sha256 = SHA256.Create();

            return sha256.ComputeHash(str.ToUtf8Bytes());
        }

        public byte[] ToSHA1()
        {
            using var sha1 = SHA1.Create();

            return sha1.ComputeHash(str.ToUtf8Bytes());
        }

        public byte[] FromBase64() =>
            Convert.FromBase64String(str);

        public byte[] FromBase64Url() =>
            Base64Url.DecodeFromChars(str);

        public byte[] ToUtf8Bytes() =>
            Encoding.UTF8.GetBytes(str);

        #endregion
    }

    extension(byte[] bytes)
    {
        public string ToBase64() =>
            Convert.ToBase64String(bytes);

        public string ToBase64Url() =>
            Base64Url.EncodeToString(bytes);

        public string ToUtf8String() =>
            Encoding.UTF8.GetString(bytes);
    }
}