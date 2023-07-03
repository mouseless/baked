using Do.Architecture;

namespace Do.Test.Blueprints.Service.Web.Phases;

public class CreateBuilder : IPhase
{
    public PhaseOrder Order => PhaseOrder.Earliest;

    public bool CanInitialize(ApplicationContext _) => true;
    public void Initialize(ApplicationContext context)
    {
        var build = WebApplication.CreateBuilder();

        context.Add(build);
        context.Add(build.Services);
    }
}
