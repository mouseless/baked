using Do.Testing;

namespace Do;

public static class ServiceSpecStringExtensions
{
    public static string AnEmail(this Stubber _) => "info@test.com";

    public static string AString(this Stubber _,
        string? value = default
    ) => value ?? "test string";

    public static Guid ToGuid(this string source) => Guid.Parse(source);
}
