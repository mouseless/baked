using Do.Architecture;

namespace Do.DependencyInjection
{
    public class DependencyInjectionLayer : ILayer
    {
        public IEnumerable<IPhase> GetPhases()
        {
            yield return new AddServices(this);
        }

        private IServiceCollection _serviceCollection;

        public ILayerTarget? GetConfigurationTarget(ApplicationContext context, IPhase phase)
        {
            if (phase.Name == nameof(AddServices))
            {
                return new LayerTarget(_serviceCollection);
            }
        }

        public class AddServices : IPhase
        {
            private readonly DependencyInjectionLayer _layer;
            public AddServices(DependencyInjectionLayer layer) => _layer = layer;

            public string Name => nameof(Build);
            public Priority Priority => Priority.High;

            public bool IsReady(ApplicationContext context) =>
                context.Has<IServiceCollection>();

            public void Initialize(ApplicationContext context)
            {
                _layer.ServiceCollection = context.Remove<IServiceCollection>();
            }
        }
    }

    public class ApplicationContext
    {
        public void Add<T>(T item) { }
        public T Remove<T>() => default!;
        public T Get<T>() => default!;
        public bool Has<T>() => false;
    }

    public interface IPhase
    {
        string Name { get; }
        bool IsReady(ApplicationContext context);
        void Initialize(ApplicationContext context);
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
