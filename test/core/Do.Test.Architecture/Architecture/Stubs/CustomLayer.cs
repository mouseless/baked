using Do.Architecture;

namespace Do;

public class CustomLayer : ILayer
{
    void ILayer.ConfigurePhases(List<IPhase> phases)
    {
        phases.Add(new AddServicesPhase());
        phases.Add(new BuildPhase());
        phases.Add(new RunPhase());
    }

    // phases should something like this;
    // AddServices
    //   Before => do stuff
    //   During => do stuff
    //   After => do stuff
    // Build
    //   Before => do stuff
    //   and so on

    void ILayer.BeforePhase(string phase, ApplicationContext context)
    {
        if (phase == "AddServices")
        {
            ...
        }
    }

    void ILayer.ApplyPhase(string phase, ApplicationContext context)
    {
        if (phase == "AddServices")
        {
            var serviceCollection = context.Get<IServiceCollection>();

            serviceCollection.AddXxx();
        }
    }

    void ILayer.AfterPhase(string phase, ApplicationContext context)
    {
        if (phase == "AddServices")
        {
            ...
        }
    }

    public class AddServicesPhase : IPhase
    {
        public void OnBegin(ApplicationContext context)
        {
            context.Add(WebApplication.CreateBuilder());
        }
    }

    public class BuildPhase : IPhase
    {
        public void OnBegin(ApplicationContext context)
        {
            var builder = context.Get<WebApplicationBuilder>();
            var app = builder.Build();

            context.Add(app);
        }
    }

    public class RunPhase : IPhase
    {
        public void OnEnd(ApplicationContext context)
        {
            var app = context.Get<WebApplication>();

            app.Run();
        }
    }
}

public class ApplicationContext { }
public interface IPhase
{
    void OnBegin(ApplicationContext context);
    void OnDispose(ApplicationContext context);
}

public static class CustomLayerExtensions
{
    public static Layers AddCustom(this Layers source) =>
        source.Add(new CustomLayer());
}
