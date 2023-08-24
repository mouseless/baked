using Do.Architecture;
using Microsoft.Extensions.Hosting;

namespace Do;

public static class EnvironmentExtensions
{
    public static TFeature ForDevelopment<TFeature>(this TFeature @default, TFeature featureOnDevelopment) where TFeature : IFeature =>
        @default.For(Environments.Development, featureOnDevelopment);

    public static TFeature ForStaging<TFeature>(this TFeature @default, TFeature featureOnStaging) where TFeature : IFeature =>
        @default.For(Environments.Staging, featureOnStaging);

    public static TFeature ForProduction<TFeature>(this TFeature @default, TFeature featureOnProduction) where TFeature : IFeature =>
        @default.For(Environments.Production, featureOnProduction);

    public static TFeature For<TFeature>(this TFeature @default, string environment, TFeature featureOnEnvironment)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == environment)
        {
            return featureOnEnvironment;
        }

        return @default;
    }
}
