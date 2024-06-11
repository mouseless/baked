using Baked.Architecture;
using Baked.Testing;
using Microsoft.Extensions.Hosting;

namespace Baked;

public static class EnvironmentExtensions
{
    public static IFeature<TConfigurator> ForNfr<TConfigurator>(this IFeature<TConfigurator> @default, IFeature<TConfigurator> featureOnNfr) =>
        @default.For(nameof(Nfr), featureOnNfr);

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

    public static bool IsNfr(this LayerConfigurator configurator) =>
        configurator.IsEnvironment(nameof(Nfr));

    public static bool IsDevelopment(this LayerConfigurator configurator) =>
        configurator.IsEnvironment(Environments.Development);

    public static bool IsStaging(this LayerConfigurator configurator) =>
        configurator.IsEnvironment(Environments.Staging);

    public static bool IsProduction(this LayerConfigurator configurator) =>
        configurator.IsEnvironment(Environments.Production);

    public static bool IsEnvironment(this LayerConfigurator _, string environment) =>
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == environment;
}