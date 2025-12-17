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
                component: () => B.ReportPage("test-page", B.PageTitle("Test Page")),
                when: c => c.Type.Is<TestPage>(),
                where: cc => cc.Path.EndsWith(nameof(Page))
            );
            builder.Conventions.AddTypeComponentConfiguration<ReportPage>(
                component: (reportPage, c, cc) => reportPage.Schema.Tabs.AddRange(
                    c.Type.GetSchemas<Tab>(cc.Drill(nameof(ReportPage.Tabs)))
                ),
                when: c => c.Type.Is<TestPage>()
            );
            builder.Conventions.AddTypeSchema(
                schema: (c, cc) => B.Tab("default"),
                when: c => c.Type.Is<TestPage>(),
                where: cc => cc.Path.EndsWith(nameof(ReportPage.Tabs))
            );
            builder.Conventions.AddTypeSchemaConfiguration<Tab>(
                schema: (tab, c, cc) => tab.Contents.Add(
                    c.Type
                        .GetMethod(nameof(TestPage.GetData))
                        .GetSchema<Tab.Content>(cc.Drill(tab.Id, nameof(Tab.Contents), 0))
                        ?? throw new($"{nameof(TestPage.GetData)} is expected to have a report page content")
                ),
                when: c => c.Type.Is<TestPage>()
            );

            builder.Conventions.AddMethodSchema(
                schema: (c, cc) => B.TabContent(component: c.Method.GetRequiredComponent(cc.Drill(nameof(Tab.Content.Component))), c.Method.Name.Kebaberize()),
                when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                where: cc => cc.Path.EndsWith(nameof(Tab.Contents), 0)
            );
            builder.Conventions.AddMethodSchemaConfiguration<Tab.Content>(
                schema: tabContent => tabContent.Narrow = true,
                when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData)
            );
            builder.Conventions.AddMethodComponent(
                component: (c, cc) => MethodText(c.Method, cc),
                when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData),
                where: cc => cc.Path.EndsWith(nameof(Tab.Content.Component))
            );
            builder.Conventions.AddMethodComponentConfiguration<Text>(
                component: (@string) => @string.Schema.MaxLength = 20,
                when: c => c.Type.Is<TestPage>() && c.Method.Name == nameof(TestPage.GetData)
            );
        });
    }
}