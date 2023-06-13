namespace Microsoft.AspNetCore.Builder;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseDo(this IApplicationBuilder source)
    {
        source.MapGet("/", () => "Hello World!");

        return source;
    }
}
