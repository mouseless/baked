using Baked.Architecture;
using Baked.CodeGeneration;
using Baked.Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using static Baked.Runtime.RuntimeLayer;

namespace Baked.Runtime;

public class RuntimeLayer : LayerBase<BuildConfiguration, AddServices, PostBuild>
{
    readonly IServiceCollection _services = new ServiceCollection();
    readonly ILoggingBuilderCollection _loggingBuilders = new LoggingBuilderCollection();

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContext<IConfigurationBuilder>(Context.GetConfigurationManager());

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.GetServiceCollection();
        services.AddLogging();
        services.AddSingleton<ServiceProviderAccessor>();

        _loggingBuilders.AddLoggingBuilder(builder => builder.ClearProviders());

        return phase.CreateContextBuilder()
            .Add(_services)
            .Add(_loggingBuilders)
            .Build()
        ;
    }

    protected override PhaseContext GetContext(PostBuild phase) =>
        phase.CreateContext(Context.GetServiceProvider());

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new BuildConfiguration();
        yield return new AddServices(_services, _loggingBuilders);
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

    public class AddServices(IServiceCollection _services, ILoggingBuilderCollection _loggingBuilders)
        : PhaseBase<DomainModel, GeneratedAssemblyProvider>(PhaseOrder.Early)
    {
        protected override void Initialize(DomainModel _, GeneratedAssemblyProvider __)
        {
            Context.Add(_services);
            Context.Add(_loggingBuilders);
        }
    }

    public class PostBuild
        : PhaseBase<IServiceProvider>
    {
        protected override void Initialize(IServiceProvider _) { }
    }
}