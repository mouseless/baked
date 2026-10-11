using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Lifetime;
using NHibernate.Util;

namespace Baked.CodingStyle.InitializableViaMethodName;

public class InitializableViaMethodNameCodingStyleFeature(IEnumerable<string> initalizerNames)
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
                    members.Has<Service>() &&
                    _initializerNames.Any(i => members.Methods.Contains(i)),
                attribute: () => new Transient(),
                order: Order.At.Infra
            );

            conventions.SetMethodAttribute(
                when: c => _initializerNames.Contains(c.Method.Name),
                attribute: () => new Initializer(),
                order: Order.At.Infra
            );

            conventions.Add(new AddInitializerParametersToQueryConvention(), order: Order.At.Infra);
            conventions.Add(new TargetUsingInitializerConvention(), order: Order.At.Max);
            conventions.Add(new RemoveInitializerNameFromRouteConvention(), order: Order.At.AbsoluteMax);
        });
    }
}