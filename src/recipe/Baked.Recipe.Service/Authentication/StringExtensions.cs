using System.Security.Cryptography;
using System.Text;

namespace System;

public static partial class StringExtensions
{
    public static byte[] ToSHA256(this string str)
    {
        using var sha256 = SHA256.Create();

        return sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
    }

    public static string ToBase64(this byte[] bytes) =>
        Convert.ToBase64String(bytes);
}