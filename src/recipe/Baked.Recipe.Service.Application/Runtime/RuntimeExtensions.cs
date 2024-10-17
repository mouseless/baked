using Baked.Architecture;
using Baked.Runtime;
using Baked.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Baked;

public static class RuntimeExtensions
{
    public static void AddRuntime(this List<ILayer> layers) =>
        layers.Add(new RuntimeLayer());

    public static IServiceCollection GetServiceCollection(this ApplicationContext context) =>
        context.Get<IServiceCollection>();

    public static IServiceProvider GetServiceProvider(this ApplicationContext context) =>
        context.Get<IServiceProvider>();

    public static void ConfigureFileProviders(this LayerConfigurator configurator, Action<IFileProviderCollection> configuration) =>
       configurator.Configure(configuration);

    public static void ConfigureLoggingBuilder(this LayerConfigurator configurator, Action<ILoggingBuilder> configuration) =>
       configurator.Configure(configuration);

    public static void ConfigureServiceCollection(this LayerConfigurator configurator, Action<IServiceCollection> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureServiceProvider(this LayerConfigurator configurator, Action<IServiceProvider> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureConfigurationBuilder(this LayerConfigurator configurator, Action<IConfigurationBuilder> configuration) =>
        configurator.Configure(configuration);

    public static void AddFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var serviceAdderType = assembly.GetExportedTypes().SingleOrDefault(t => t.IsAssignableTo(typeof(IServiceAdder))) ?? throw new("`IServiceAdder` implementation not found");
        var serviceAdder = (IServiceAdder?)Activator.CreateInstance(serviceAdderType) ?? throw new($"Cannot create instance of {serviceAdderType}");

        serviceAdder.AddServices(services);
    }

    public static void AddEmbedded(this IFileProviderCollection providers, Assembly assembly, string? baseNamespace) =>
        providers.Add(new EmbeddedFileProvider(assembly, baseNamespace));

    public static void AddPhysical(this IFileProviderCollection providers, string root) =>
        providers.Add(new PhysicalFileProvider(root));

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

    public static IServiceProvider UsingCurrentScope(this IServiceProvider sp) =>
        sp.GetRequiredService<ServiceProviderAccessor>().GetServiceProvider() ?? sp;

    public static IServiceProvider TheServiceProvider(this Stubber giveMe) =>
        giveMe.Spec.Context.GetServiceProvider().UsingCurrentScope();

    public static T The<T>(this Stubber giveMe) where T : notnull =>
        giveMe.TheServiceProvider().GetRequiredService<T>();

    public static object The(this Stubber giveMe, Type type) =>
        giveMe.TheServiceProvider().GetRequiredService(type);

    public static T An<T>(this Stubber giveMe) where T : notnull =>
        giveMe.TheServiceProvider().GetRequiredService<T>();

    public static object An(this Stubber giveMe, Type type) =>
        giveMe.TheServiceProvider().GetRequiredService(type);

    public static T A<T>(this Stubber giveMe) where T : notnull =>
        giveMe.TheServiceProvider().GetRequiredService<T>();

    public static object A(this Stubber giveMe, Type type) =>
        giveMe.TheServiceProvider().GetRequiredService(type);
}