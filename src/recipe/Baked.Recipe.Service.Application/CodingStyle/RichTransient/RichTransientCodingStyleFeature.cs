using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;

namespace Baked.CodingStyle.RichTransient;

public class RichTransientCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeMetadata(new RichTransientAttribute(),
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                        members.Has<TransientAttribute>() &&
                        members.Has<ServiceAttribute>() &&
                        members.Methods.TryGetValue("With", out var method) &&
                            method.DefaultOverload.IsPublic &&
                            method.DefaultOverload.Parameters.Count == 1 &&
                            method.DefaultOverload.Parameters.Any(p =>
                                p.Name == "id" &&
                                (p.ParameterType.IsValueType || p.ParameterType.Is<string>())
                            ),
                order: 40
            );
            builder.Conventions.AddMethodMetadata(new ApiMethodAttribute(),
                when: c =>
                    c.Type.Has<HasPublicDataAttribute>() &&
                    c.Method.Has<InitializerAttribute>() &&
                    c.Method.DefaultOverload.IsPublic &&
                    c.Type.Has<RichTransientAttribute>(),
                order: 50
            );
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            var domainModel = configurator.Context.GetDomainModel();

            conventions.Add(new TargetRichTransientFromRouteConvention());
            conventions.Add(new RichTransientInitializerIsGetResourceConvention());
        });
    }
}