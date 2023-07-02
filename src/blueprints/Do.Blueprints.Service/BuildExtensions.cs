using Do.Architecture;

namespace Do;

public static class BuildExtensions
{
    public static Application AsService(this Build source,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        configure ??= _ => { };

        return source.As(app =>
            {
                configure(app);
            });
    }
}
