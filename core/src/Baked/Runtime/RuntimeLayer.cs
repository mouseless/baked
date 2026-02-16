using Baked.Architecture;
using Baked.CodeGeneration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

using static Baked.Runtime.RuntimeLayer;

namespace Baked.Runtime;

public class RuntimeLayer : LayerBase<BuildConfiguration, AddServices, PostBuild>
{
    public const string FileProvidersKey = "CompositeFileProvider";

    readonly IServiceCollection _services = new ServiceCollection();
    readonly ILoggingBuilder _loggingBuilder;
    readonly ThreadOptions _threadOptions = new();

    public RuntimeLayer()
    {
        _loggingBuilder = new LoggingBuilder(_services);
    }

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContext<IConfigurationBuilder>(Context.GetConfigurationManager());

    protected override PhaseContext GetContext(AddServices phase)
    {
        _services.AddLogging();
        _services.AddSingleton<ServiceProviderAccessor>();

        return phase.CreateContextBuilder()
            .Add(_services)
            .Add(_loggingBuilder)
            .Add(_threadOptions)
            .OnDispose(() =>
            {
                _services.AddSingleton<IFileProvider>(sp =>
                    new CompositeFileProvider(sp.UsingCurrentScope().GetKeyedServices<IFileProvider>(FileProvidersKey))
                );

                if (_threadOptions.MinThreadCount.HasValue)
                {
                    ThreadPool.SetMinThreads(_threadOptions.MinThreadCount.Value, _threadOptions.MinThreadCount.Value * 2);
                }

                if (_threadOptions.MaxThreadCount.HasValue)
                {
                    ThreadPool.SetMaxThreads(_threadOptions.MaxThreadCount.Value, _threadOptions.MaxThreadCount.Value * 2);
                }
            })
            .Build()
        ;
    }

    protected override PhaseContext GetContext(PostBuild phase) =>
        phase.CreateContext(Context.GetServiceProvider());

    protected override IEnumerable<IPhase> GetStartPhases()
    {
        yield return new BuildConfiguration();
        yield return new AddServices(_services);
        yield return new PostBuild();
    }

    public class BuildConfiguration()
        : PhaseBase<ConfigurationManager>(PhaseOrder.Earliest)
    {
        protected override void Initialize(ConfigurationManager configurationManager)
        {
            Settings.SetConfigurationRoot(configurationManager);
        }
    }

    public class AddServices(IServiceCollection _services)
        : PhaseBase<GeneratedContext>(PhaseOrder.Early)
    {
        protected override void Initialize(GeneratedContext __)
        {
            Context.Add(_services);
        }
    }

    public class PostBuild : PhaseBase<IServiceProvider>
    {
        protected override void Initialize(IServiceProvider _) { }
    }
}