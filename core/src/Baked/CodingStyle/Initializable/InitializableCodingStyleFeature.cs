using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Lifetime;
using NHibernate.Util;

namespace Baked.CodingStyle.Initializable;

public class InitializableCodingStyleFeature(IEnumerable<string> initalizerNames)
    : IFeature<CodingStyleConfigurator>
{
    readonly HashSet<string> _initializerNames = [.. initalizerNames];

    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    _initializerNames.Any(i => members.Methods.Contains(i)),
                attribute: () => new TransientAttribute(),
                order: Order.At.Defaults
            );

            conventions.SetMethodAttribute(
                when: c => _initializerNames.Contains(c.Method.Name),
                attribute: () => new InitializerAttribute(),
                order: Order.At.Defaults
            );

            conventions.Add(new AddInitializerParametersToQueryConvention(), order: Order.At.Defaults);
            conventions.Add(new TargetUsingInitializerConvention(), order: Order.At.Max);
            conventions.Add(new RemoveInitializerNameFromRouteConvention(), order: Order.At.AbsoluteMax);
        });
    }
}