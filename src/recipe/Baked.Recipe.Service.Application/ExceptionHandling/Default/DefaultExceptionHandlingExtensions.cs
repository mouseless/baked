using Baked.Configuration;
using Baked.ExceptionHandling;
using Baked.ExceptionHandling.Default;

namespace Baked;

public static class DefaultExceptionHandlingExtensions
{
    public static DefaultExceptionHandlingFeature Default(this ExceptionHandlingConfigurator _,
        Setting<string>? typeUrlFormat = default
    ) => new(typeUrlFormat);
}