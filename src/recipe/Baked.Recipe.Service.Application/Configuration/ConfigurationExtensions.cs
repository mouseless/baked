using Baked.Architecture;
using Baked.Configuration;
using Baked.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Baked;

public static class ConfigurationExtensions
{
    public static void AddConfiguration(this List<ILayer> layers) =>
        layers.Add(new ConfigurationLayer());

    public static void ConfigureConfigurationBuilder(this LayerConfigurator configurator, Action<IConfigurationBuilder> configuration) =>
        configurator.Configure(configuration);

    public static void AddJson(this IConfigurationBuilder builder, string json) =>
        builder.Add(new JsonConfigurationSource(json));

    public static void AddJsonAsDefault(this IConfigurationBuilder builder, string json) =>
        builder.Sources.Insert(
            Math.Max(builder.Sources.Count - 4, 0), // try to insert before appsetttings.json + appsettings.[Environment].json + 2 more default configurations
            new JsonConfigurationSource(json)
        );

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