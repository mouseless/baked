using System.Security.Cryptography;
using System.Text;

namespace System;

public static class StringExtensions
{
    public static byte[] ToSHA256(this string source)
    {
        using var sha256 = SHA256.Create();

        return sha256.ComputeHash(Encoding.UTF8.GetBytes(source));
    }

    public static string ToBase64(this byte[] source) =>
        Convert.ToBase64String(source);
}
