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
            builder.Conventions.AddTypeMetadata(
                apply: (c, add) =>
                {
                    add(c.Type, new ApiInputAttribute());
                    add(c.Type, new LocatableAttribute());
                },
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    members.Has<TransientAttribute>() &&
                    members.Methods.Any(m =>
                        m.Has<InitializerAttribute>() &&
                        m.DefaultOverload.IsPublic &&
                        m.DefaultOverload.Parameters.Count == 1 &&
                        m.DefaultOverload.Parameters.All(p =>
                            p.Name == "id" &&
                            (p.ParameterType.IsValueType || p.ParameterType.Is<string>())
                        )
                    ),
                order: 10
            );
            builder.Conventions.AddMethodMetadata(new ApiMethodAttribute(),
                when: c =>
                    c.Type.Has<TransientAttribute>() &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.Any(p => p.IsPublic) &&
                    c.Method.Has<InitializerAttribute>() &&
                    c.Method.DefaultOverload.IsPublic &&
                    c.Method.DefaultOverload.Parameters.Count == 1 &&
                    c.Method.DefaultOverload.Parameters.All(p =>
                        p.Name == "id" && (p.ParameterType.IsValueType || p.ParameterType.Is<string>())
                    ),
                order: 20
            );
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new FindTargetFromInitializerConvention(), order: -10);
            conventions.Add(new InitializeUsingQueryParametersConvention());
            conventions.Add(new InitializeUsingIdParameterConvention());
            conventions.Add(new RichTransientInitializerIsGetResourceConvention());
            conventions.Add(new LookUpTransientByIdConvention());
            conventions.Add(new LookUpTransientsByIdsConvention());
        });
    }
}