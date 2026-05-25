using Baked.Architecture;
using Baked.Playground.Theme;
using Baked.Theme;
using Baked.Ui;
using Humanizer;

using static Baked.Playground.Theme.Custom.DomainComponents;

using B = Baked.Ui.Components;

namespace Baked.Playground.Override.Domain;

public class TestPageDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainConventions(conventions =>
        {
            conventions.AddTypeComponent(
                when: c => c.Type.Is<TestPage>(),
                where: cc => cc.Path.EndsWith(nameof(Page)),
                component: () => B.TabbedPage("test-page", B.PageTitle("Test Page"))
            );
            conventions.AddTypeComponentConfiguration<TabbedPage>(
                when: c => c.Type.Is<TestPage>(),
                component: (tp, c, cc) => tp.Schema.Tabs.AddRange(
                    c.Type.GenerateSchemas<Tab>(cc.Drill(nameof(TabbedPage.Tabs)))
                )
            );
            conventions.AddTypeSchema(
                when: c => c.Type.Is<TestPage>(),
                where: cc => cc.Path.EndsWith(nameof(TabbedPage.Tabs)),
                schema: (c, cc) => B.Tab("default")
            );
            conventions.AddTypeSchemaConfiguration<Tab>(
                when: c => c.Type.Is<TestPage>(),
                schema: (t, c, cc) => t.Contents.Add(
                    c.Type
                        .GetMethod(nameof(TestPage.GetData))
                        .GenerateRequiredSchema<Content>(cc.Drill(t.Id, nameof(Tab.Contents), 0))
                )
            );

            conventions.AddMethodSchema(
                when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                where: cc => cc.Path.EndsWith(nameof(Tab.Contents), 0),
                schema: (c, cc) => B.Content(component: c.Method.GenerateRequiredComponent(cc.Drill(nameof(Content.Component))), c.Method.Name.Kebaberize())
            );
            conventions.AddMethodSchemaConfiguration<Content>(
                when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                schema: tabContent => tabContent.Narrow = true
            );
            conventions.AddMethodComponent(
                when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                where: cc => cc.Path.EndsWith(nameof(Content.Component)),
                component: (c, cc) => MethodText(c.Method, cc)
            );
            conventions.AddMethodComponentConfiguration<Text>(
                when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                component: t => t.Schema.MaxLength = 20
            );
        });
    }
}