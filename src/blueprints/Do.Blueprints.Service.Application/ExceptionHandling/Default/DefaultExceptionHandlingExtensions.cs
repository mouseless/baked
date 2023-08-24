using Do.ExceptionHandling;
using Do.ExceptionHandling.Default;

namespace Do;

public static class DefaultExceptionHandlingExtensions
{
    public static IExceptionHandlingFeature Default(this ExceptionHandlingConfigurator _,
        Action<Options>? optionsBuilder = default
    )
    {
        optionsBuilder ??= _ => { };

        var options = new Options();
        optionsBuilder(options);

        return new DefaultExceptionHandlingFeature(options.Handlers);
    }

    public static void AddHandler<T>(this Options source) => source.Handlers.Add(typeof(T));
}
