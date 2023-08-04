using Do.ExceptionHandling;
using Do.ExceptionHandling.Default;

namespace Do;

public static class DefaultExceptionHandlingExtensions
{
    public static DefaultExceptionHandlingFeature Default(this ExceptionHandlingConfigurator source,
        Action<Options>? optionsBuilder = default
    )
    {
        optionsBuilder ??= _ => { };

        var options = new Options();
        optionsBuilder(options);

        return new(options.Handlers);
    }

    public static void AddHandler<T>(this Options source) => source.Handlers.Add(typeof(T));
}
