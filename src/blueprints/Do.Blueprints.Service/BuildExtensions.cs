using Do.Architecture;

namespace Do;

public static class BuildExtensions
{
    public static IRunnable AsService(this Build source,
        Action<Application>? configure = default
    )
    {
        configure ??= _ => { };

        return source.As(app =>
            {
                /*
                var builder = WebApplication.CreateBuilder();
                var app = builder.Build();
                */

                configure(app);
            });
    }
}
