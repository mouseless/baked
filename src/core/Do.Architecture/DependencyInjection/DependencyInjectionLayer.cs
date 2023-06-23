using Do.Architecture;

namespace Do.DependencyInjection
{
    public class DependencyInjectionLayer : ILayer
    {
        public void Configure(object context)
        {
            // add AddServicesPhase
        }

        public object GetConfigurationTarget()
        {
            // return service collection
            return new();
        }
    }

}

namespace Do
{
    using Do.DependencyInjection;

    public static class DependencyInjectionExtensions
    {
        public static void AddDependencyInjection(this ICollection<ILayer> layers) => layers.Add(new DependencyInjectionLayer());
    }
}

/*
public class AddServicesPhase // : IPhase
{
    public bool IsReady(object context) => true;

    public void Begin(object context)
    {
        // add ServiceCollection to context
    }

    public void Dispose()
    {
        // remove ServiceCollection from context
    }
}
*/
