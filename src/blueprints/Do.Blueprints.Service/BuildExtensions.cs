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
                configure(app);
            });
    }
}
