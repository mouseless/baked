using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Theme;

using static Baked.Theme.Admin.DomainComponents;

namespace Baked.Ux.TypeWithOnlyGetIsReportPage;

public class TypeWithOnlyGetIsReportPageUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeComponent(
                component: (c, cc) => TypeReportPage(c.Type, cc),
                whenType: c =>
                    c.Type.Has<ControllerModelAttribute>() &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Methods.Having<ActionModelAttribute>().All(m => m.GetAction().Method == HttpMethod.Get),
                whenComponent: cc => cc.Path.Is(nameof(Page))
            );
            builder.Conventions.AddMethodSchema(
                schema: (c, cc) => MethodReportPageTabContent(c.Method, cc),
                whenMethod: c => c.Method.Has<ActionModelAttribute>()
            );
        });
    }
}