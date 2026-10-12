using Baked.Architecture;
using Baked.Business;
using Baked.CodingStyle.InitializableViaMethodName;
using Baked.Domain.Configuration;
using Baked.Lifetime;
using NHibernate.Util;

namespace Baked;

public static class InitializableViaMethodNameCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> InitializableViaMethodName(this CodingStyleFeature.Configurator x,
        string[]? initializerNames = default
    ) => x.Define(new HashSet<string>(initializerNames ?? ["With"]), (configurator, initializerNames) =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<Service>() &&
                    initializerNames.Any(i => members.Methods.Contains(i)),
                attribute: () => new Transient(),
                order: Order.At.Infra
            );

            conventions.SetMethodAttribute(
                when: c => initializerNames.Contains(c.Method.Name),
                attribute: () => new Initializer(),
                order: Order.At.Infra
            );

            conventions.Add(new AddInitializerParametersToQueryConvention(), order: Order.At.Infra);
            conventions.Add(new TargetUsingInitializerConvention(), order: Order.At.Max);
            conventions.Add(new RemoveInitializerNameFromRouteConvention(), order: Order.At.AbsoluteMax);
        });
    });
}