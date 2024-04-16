using Do.Architecture;
using Microsoft.Extensions.Hosting;

namespace Do;

public static class EnvironmentExtensions
{
    public static IFeature<TConfigurator> ForDevelopment<TConfigurator>(this IFeature<TConfigurator> @default, IFeature<TConfigurator> featureOnDevelopment) =>
        @default.For(Environments.Development, featureOnDevelopment);

    public static IFeature<TConfigurator> ForStaging<TConfigurator>(this IFeature<TConfigurator> @default, IFeature<TConfigurator> featureOnStaging) =>
        @default.For(Environments.Staging, featureOnStaging);

    public static IFeature<TConfigurator> ForProduction<TConfigurator>(this IFeature<TConfigurator> @default, IFeature<TConfigurator> featureOnProduction) =>
        @default.For(Environments.Production, featureOnProduction);

    public static IFeature<TConfigurator> For<TConfigurator>(this IFeature<TConfigurator> @default, string environment, IFeature<TConfigurator> featureOnEnvironment)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == environment)
        {
            return featureOnEnvironment;
        }

        return @default;
    }
}