using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.RichTransient;

public class RichTransientCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeAttribute(
                apply: (c, set) =>
                {
                    set(c.Type, new ApiInputAttribute());
                    set(c.Type, new LocatableAttribute());
                    set(c.Type, new IdAttribute(typeof(string), "Id", "Id".Kebaberize()));
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
            builder.Conventions.SetMethodAttribute(
                attribute: c => new ActionModelAttribute(),
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
            builder.Conventions.SetPropertyAttribute(
                apply: (c, set) =>
                {
                    c.Property.PropertyType.Apply(t =>
                    {
                        set(c.Property, new IdAttribute(t, "Id", "Id".Kebaberize()));
                    });
                },
                when: c => c.Property.Name == "Id"
            );

            builder.Conventions.Add(new RichTransientUnderPluralGroupConvention());
            builder.Conventions.Add(new AddInitializerParametersToQueryConvention());
            builder.Conventions.Add(new AddIdParameterToRouteConvention());
            builder.Conventions.Add(new LookupRichTransientByIdConvention());
            builder.Conventions.Add(new LookupRichTransientsByIdsConvention());
            builder.Conventions.Add(new RichTransientInitializerIsGetResourceConvention());
            builder.Conventions.Add(new FindTargetUsingInitializerConvention(), order: 10);
        });
    }
}