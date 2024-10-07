using Baked.Architecture;
using Baked.CodeGeneration;
using Baked.Domain.Model;
using Baked.Runtime.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using static Baked.HttpServer.HttpServerLayer;
using static Baked.Runtime.RuntimeLayer;

namespace Baked.Runtime;

public class RuntimeLayer : LayerBase<CreateBuilder, BuildConfiguration, AddServices, PostBuild>
{
    readonly IServiceCollection _services = new ServiceCollection();

    protected override PhaseContext GetContext(CreateBuilder phase)
    {
        var builder = Context.GetWebApplicationBuilder();

        builder.Logging.ClearProviders();

        return phase.CreateContext(builder.Logging);
    }

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase.CreateContext<IConfigurationBuilder>(Context.GetConfigurationManager());

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.GetServiceCollection();

        services.AddLogging();

        return phase.CreateContext(_services);
    }

    protected override PhaseContext GetContext(PostBuild phase) =>
        phase.CreateContext(Context.GetServiceProvider());

    protected override IEnumerable<IPhase> GetPhases()
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
        : PhaseBase<DomainModel, GeneratedAssemblyProvider>(PhaseOrder.Early)
    {
        protected override void Initialize(DomainModel _, GeneratedAssemblyProvider __)
        {
            Context.Add(_services);
        }
    }

    public class PostBuild
        : PhaseBase<IServiceProvider>
    {
        protected override void Initialize(IServiceProvider _) { }
    }
}