namespace Microsoft.AspNetCore.Builder;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseDo(this IApplicationBuilder source)
    {
        if (source is WebApplication web)
        {
            web.MapGet("/", () => "Hello World!");
        }

        return source;
    }
}
