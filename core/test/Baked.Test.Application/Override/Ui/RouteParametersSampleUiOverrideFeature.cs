using Baked.Architecture;
using Baked.Business;
using Baked.RestApi.Model;
using Baked.Test.Theme;
using Baked.Theme;
using Baked.Ui;

using static Baked.Theme.Default.DomainComponents;

namespace Baked.Test.Override.Ui;

public class RouteParametersSampleUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // Route parameters sample
            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<RouteParametersSample>() && c.Method.DefaultOverload.ReturnsList(),
                where: cc => cc.Path.EndsWith(nameof(Page), nameof(RouteParametersSample), nameof(SimplePage), nameof(SimplePage.Contents), "*"),
                component: (c, cc) => MethodDataPanel(c.Method, cc)
            );

            builder.Conventions.AddTypeComponentConfiguration<SimplePage>(
                when: c => c.Type.Is<RouteParametersSample>(),
                component: (sp, c, cc) =>
                {
                    cc = cc.Drill(nameof(SimplePage.Contents));

                    foreach (var method in c.Type.GetMembers().Methods.Having<ActionModelAttribute>())
                    {
                        if (method.Has<InitializerAttribute>()) { continue; }
                        if (method.GetAction().Method != HttpMethod.Get) { continue; }

                        var content = method.GetSchema<Content>(cc.Drill(sp.Schema.Contents.Count));
                        if (content is null) { continue; }

                        sp.Schema.Contents.Add(content);
                    }
                }
            );
        });
    }
}