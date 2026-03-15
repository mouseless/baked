using Baked.Architecture;
using Baked.Binding;

namespace Baked;

public static class BindingExtensions
{
    extension(List<IFeature> features)
    {
        public void AddBinding(Func<BindingConfigurator, IFeature<BindingConfigurator>> configure) =>
            features.Add(configure(new()));
    }
}