namespace Microsoft.AspNetCore.Builder;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseDo(this WebApplication source)
    {
        source.MapGet("/", () => "Hello World!");

        return source;
    }
}
