using Baked.Architecture;
using Baked.Test.Theme;
using Baked.Test.Ui;
using Baked.Theme;

using static Baked.Test.Theme.Custom.DomainComponents;
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
                where: cc => cc.Path.EndsWith(nameof(Page), nameof(RouteParametersSample), nameof(ContainerPage), nameof(ContainerPage.Contents), "*"),
                component: (c, cc) => MethodDataPanel(c.Method, cc)
            );

            builder.Conventions.AddTypeComponent(
                when: c => c.Type.Is<RouteParametersSample>(),
                where: cc => cc.Path.EndsWith(nameof(Page), nameof(RouteParametersSample)),
                component: (c, cc) => TypeContainerPage(c.Type, cc)
            );

            builder.Conventions.AddTypeComponentConfiguration<ContainerPage>(
                when: c => c.Type.Is<RouteParametersSample>(),
                where: cc => true,
                component: (container, c, cc) =>
                {
                    cc = cc.Drill(nameof(ContainerPage), nameof(ContainerPage.Contents));

                    foreach (var method in c.Type.GetMembers().Methods)
                    {
                        var component = method.GetComponent(cc.Drill(container.Schema.Contents.Count));
                        if (component is null) { continue; }

                        container.Schema.Contents.Add(component);
                    }
                },
                order: int.MaxValue
            );
        });
    }
}