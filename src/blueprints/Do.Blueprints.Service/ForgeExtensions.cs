using Do.Architecture;

namespace Do;

public static class ForgeExtensions
{
    public static Application Service(this Forge source,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        configure ??= _ => { };

        return source.Application(app =>
            {
                configure(app);
            });
    }
}
