using Do.Architecture;
using Microsoft.AspNetCore.Builder;

namespace Do;

public static class BuildExtensions
{
    public static IRunnable AsService(this Build source) =>
        source.As(_ =>
        {
            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();
            app.MapGet("/", () => "Hello World!");
        });
}
