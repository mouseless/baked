using Baked.Architecture;
using Baked.Binding;

namespace Baked;

public static class BindingExtensions
{
    public static void AddBinding(this List<IFeature> features, Func<BindingConfigurator, IFeature<BindingConfigurator>> configure) =>
        features.Add(configure(new()));
}