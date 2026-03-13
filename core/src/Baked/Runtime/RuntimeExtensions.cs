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
    public class Configurator(LayerConfigurator _configurator)
    {
        public void ConfigureLoggingBuilder(Action<ILoggingBuilder> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureServiceCollection(Action<IServiceCollection> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureServiceProvider(Action<IServiceProvider> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureConfigurationBuilder(Action<IConfigurationBuilder> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureThreadOptions(Action<ThreadOptions> configuration) =>
            _configurator.Configure(configuration);
    }

    extension(LayerConfigurator configurator)
    {
        public Configurator Runtime => new(configurator);

        public bool IsNfr =>
            configurator.IsEnvironment(nameof(Nfr));

        public bool IsDevelopment =>
            configurator.IsEnvironment(Environments.Development);

        public bool IsStaging =>
            configurator.IsEnvironment(Environments.Staging);

        public bool IsProduction =>
            configurator.IsEnvironment(Environments.Production);

        public bool IsEnvironment(string environment) =>
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == environment;
    }

    extension(List<ILayer> layers)
    {
        public void AddRuntime() =>
            layers.Add(new RuntimeLayer());
    }

    extension(ApplicationContext context)
    {
        public IServiceCollection GetServiceCollection() =>
            context.Get<IServiceCollection>();

        public IServiceProvider GetServiceProvider() =>
            context.Get<IServiceProvider>();
    }

    extension(IServiceCollection services)
    {
        public void AddFromAssembly(Assembly assembly)
        {
            var serviceAdder = assembly.CreateRequiredImplementationInstance<IServiceAdder>();

            serviceAdder.AddServices(services);
        }

        public void AddFileProvider(IFileProvider implementation)
        {
            services.AddKeyedSingleton(RuntimeLayer.FileProvidersKey, implementation);
            services.AddSingleton(implementation);
        }
    }

    extension(IConfigurationBuilder builder)
    {
        public void AddJson(string json) =>
            builder.Add(new JsonConfigurationSource(json));

        public void AddJsonAsDefault(string json) =>
            builder.Sources.Insert(
                Math.Max(builder.Sources.Count - 4, 0), // try to insert before appsetttings.json + appsettings.[Environment].json + 2 more default configurations
                new JsonConfigurationSource(json)
            );
    }

    extension(Assembly assembly)
    {
        public T? CreateImplementationInstance<T>()
        {
            var instanceType = assembly.GetExportedTypes().SingleOrDefault(t => t.IsAssignableTo(typeof(T)));
            if (instanceType == null) { return default; }

            return (T?)Activator.CreateInstance(instanceType) ?? throw new($"Cannot create instance of {instanceType}");
        }

        public T CreateRequiredImplementationInstance<T>() =>
            assembly.CreateImplementationInstance<T>() ?? throw new($"`{typeof(T)}` implementation not found");
    }

    extension<TConfigurator>(IFeature<TConfigurator> @default)
    {
        public IFeature<TConfigurator> ForNfr(IFeature<TConfigurator> featureOnNfr) =>
            @default.For(nameof(Nfr), featureOnNfr);

        public IFeature<TConfigurator> ForDevelopment(IFeature<TConfigurator> featureOnDevelopment) =>
            @default.For(Environments.Development, featureOnDevelopment);

        public IFeature<TConfigurator> ForStaging(IFeature<TConfigurator> featureOnStaging) =>
            @default.For(Environments.Staging, featureOnStaging);

        public IFeature<TConfigurator> ForProduction(IFeature<TConfigurator> featureOnProduction) =>
            @default.For(Environments.Production, featureOnProduction);

        public IFeature<TConfigurator> For(string environment, IFeature<TConfigurator> featureOnEnvironment)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == environment)
            {
                return featureOnEnvironment;
            }

            return @default;
        }
    }

    extension(IServiceProvider sp)
    {
        public IServiceProvider UsingCurrentScope() =>
            sp.GetRequiredService<ServiceProviderAccessor>().GetServiceProvider() ?? sp;
    }

    extension(Stubber giveMe)
    {
        public IServiceProvider TheServiceProvider() =>
            giveMe.Spec.StartContext.GetServiceProvider().UsingCurrentScope();

        public T The<T>() where T : notnull =>
            giveMe.TheServiceProvider().GetRequiredService<T>();

        public object The(Type type) =>
            giveMe.TheServiceProvider().GetRequiredService(type);

        public T An<T>() where T : notnull =>
            giveMe.TheServiceProvider().GetRequiredService<T>();

        public object An(Type type) =>
            giveMe.TheServiceProvider().GetRequiredService(type);

        public T A<T>() where T : notnull =>
            giveMe.TheServiceProvider().GetRequiredService<T>();

        public object A(Type type) =>
            giveMe.TheServiceProvider().GetRequiredService(type);
    }
}