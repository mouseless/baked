using Do.Architecture;
using Microsoft.Extensions.Hosting;

namespace Do;

public static class EnvironmentExtensions
{
    public static TFeature ForDevelopment<TFeature>(this IFeature @default, TFeature featureOnDevelopment) where TFeature : IFeature =>
        (TFeature)@default.For(Environments.Development, featureOnDevelopment);

    public static TFeature ForStaging<TFeature>(this IFeature @default, TFeature featureOnStaging) where TFeature : IFeature =>
        (TFeature)@default.For(Environments.Staging, featureOnStaging);

    public static TFeature ForProduction<TFeature>(this IFeature @default, TFeature featureOnProduction) where TFeature : IFeature =>
        (TFeature)@default.For(Environments.Production, featureOnProduction);

    public static IFeature For(this IFeature @default, string environment, IFeature featureOnEnvironment)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == environment)
        {
            return featureOnEnvironment;
        }

        return @default;
    }
}
