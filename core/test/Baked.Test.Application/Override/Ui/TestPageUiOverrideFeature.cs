using Baked.Architecture;
using Baked.Test.Theme;
using Baked.Theme;
using Baked.Ui;
using Humanizer;

using static Baked.Test.Theme.Custom.DomainComponents;

using B = Baked.Ui.Components;

namespace Baked.Test.Override.Ui;

public class TestPageUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeComponent(
                when: c => c.Type.Is<TestPage>(),
                where: cc => cc.Path.EndsWith(nameof(Page)),
                component: () => B.TabbedPage("test-page", B.PageTitle("Test Page"))
            );
            builder.Conventions.AddTypeComponentConfiguration<TabbedPage>(
                when: c => c.Type.Is<TestPage>(),
                component: (tp, c, cc) => tp.Schema.Tabs.AddRange(
                    c.Type.GetSchemas<Tab>(cc.Drill(nameof(TabbedPage.Tabs)))
                )
            );
            builder.Conventions.AddTypeSchema(
                when: c => c.Type.Is<TestPage>(),
                where: cc => cc.Path.EndsWith(nameof(TabbedPage.Tabs)),
                schema: (c, cc) => B.Tab("default")
            );
            builder.Conventions.AddTypeSchemaConfiguration<Tab>(
                when: c => c.Type.Is<TestPage>(),
                schema: (t, c, cc) => t.Contents.Add(
                    c.Type
                        .GetMethod(nameof(TestPage.GetData))
                        .GetRequiredSchema<Content>(cc.Drill(t.Id, nameof(Tab.Contents), 0))
                )
            );

            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                where: cc => cc.Path.EndsWith(nameof(Tab.Contents), 0),
                schema: (c, cc) => B.Content(component: c.Method.GetRequiredComponent(cc.Drill(nameof(Content.Component))), c.Method.Name.Kebaberize())
            );
            builder.Conventions.AddMethodSchemaConfiguration<Content>(
                when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                schema: tabContent => tabContent.Narrow = true
            );
            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                where: cc => cc.Path.EndsWith(nameof(Content.Component)),
                component: (c, cc) => MethodText(c.Method, cc)
            );
            builder.Conventions.AddMethodComponentConfiguration<Text>(
                when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                component: (@string) => @string.Schema.MaxLength = 20
            );
        });
    }
}