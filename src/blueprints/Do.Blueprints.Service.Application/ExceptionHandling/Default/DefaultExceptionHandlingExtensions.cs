using Do.ExceptionHandling;
using Do.ExceptionHandling.Default;

namespace Do;

public static class DefaultExceptionHandlingExtensions
{
    public static DefaultExceptionHandlingFeature Default(this ExceptionHandlingConfigurator _) => new();
}
