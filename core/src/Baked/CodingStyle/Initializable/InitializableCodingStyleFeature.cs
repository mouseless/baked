using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;
using Baked.RestApi;
using NHibernate.Util;

namespace Baked.CodingStyle.Initializable;

public class InitializableCodingStyleFeature(IEnumerable<string> initalizerNames)
    : IFeature<CodingStyleConfigurator>
{
    readonly HashSet<string> _initializerNames = [.. initalizerNames];

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeAttribute(
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    _initializerNames.Any(i => members.Methods.Contains(i)),
                attribute: () => new TransientAttribute()
            );
            builder.Conventions.SetMethodAttribute(
                when: c => _initializerNames.Contains(c.Method.Name),
                attribute: () => new InitializerAttribute()
            );

            builder.Conventions.Add(new AddInitializerParametersToQueryConvention());
            builder.Conventions.Add(new TargetUsingInitializerConvention(), order: RestApiLayer.MaxConventionOrder - 10);
            builder.Conventions.Add(new RemoveInitializerNameFromRouteConvention(), order: RestApiLayer.MaxConventionOrder);
        });
    }
}