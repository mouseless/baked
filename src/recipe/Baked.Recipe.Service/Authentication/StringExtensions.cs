using System.Security.Cryptography;
using System.Text;

namespace System;

public static partial class StringExtensions
{
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

    public static byte[] ToUtf8Bytes(this string @string) =>
        Encoding.UTF8.GetBytes(@string);

    public static string ToUtf8String(this byte[] bytes) =>
        Encoding.UTF8.GetString(bytes);

    public static string ToBase64(this byte[] bytes, bool urlEncode)
    {
        if (!urlEncode) { return bytes.ToBase64(); }

        return bytes
            .ToBase64()
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }

    public static byte[] FromBase64(this string @string,
        bool urlDecode = false
    )
    {
        if (urlDecode)
        {
            @string = @string.Replace('_', '/').Replace('-', '+');
            switch (@string.Length % 4)
            {
                case 2:
                    @string += "==";
                    break;
                case 3:
                    @string += "=";
                    break;
                default:
                    break;
            }
        }

        return Convert.FromBase64String(@string);
    }
}