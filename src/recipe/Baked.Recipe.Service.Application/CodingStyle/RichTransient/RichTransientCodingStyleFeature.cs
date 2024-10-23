using Baked.Architecture;
using Baked.Business;

namespace Baked.CodingStyle.RichTransient;

public class RichTransientCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeMetadata(new LocatableAttribute(),
                when: c =>
                    c.Type.IsClass && !c.Type.IsAbstract &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    members.Methods.TryGetValue("With", out var method) && method.Overloads.Any(o => o.IsPublic && o.Parameters.All(p => p.Name == "id")),
                order: int.MaxValue
            );
            builder.Conventions.AddMethodMetadata(new ApiMethodAttribute(),
                when: c => c.Method.Has<InitializerAttribute>() && c.Type.Has<LocatableAttribute>() && c.Method.Overloads.Any(o => o.IsPublic),
                order: int.MaxValue
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