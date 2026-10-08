using Baked.Architecture;
using Baked.Domain.Configuration;
using Baked.Playground.Theme;
using Baked.Theme;
using Baked.Ui;

using static Baked.Playground.Theme.Custom.DomainComponents;

using B = Baked.Ui.Components;

namespace Baked.Playground.Override.Domain;

public class TestPageDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddTypeComponent(
                when: c => c.Type.Is<TestPage>(),
                where: cc => cc.Path.EndsWith(nameof(Page)),
                component: () => B.TabbedPage("test-page"),
                order: Order.At.Override
            );
            conventions.AddTypeComponentConfiguration<TabbedPage>(
                when: c => c.Type.Is<TestPage>(),
                component: (tp, c, cc) =>
                {
                    tp.Schema.Title?.Data = Datas.Inline("Test Page");
                    tp.Schema.Tabs.Add(
                        c.Type.GenerateRequiredSchema<Tab>(cc.Drill("tabs", "default"))
                    );
                },
                order: Order.At.Override
            );
            conventions.AddTypeSchema(
                when: c => c.Type.Is<TestPage>(),
                where: cc => cc.Path.EndsWith("tabs", "default"),
                schema: (c, cc) => B.Tab(),
                order: Order.At.Override
            );
            conventions.AddTypeSchemaConfiguration<Tab>(
                when: c => c.Type.Is<TestPage>(),
                where: cc => cc.Path.EndsWith("tabs", "default"),
                schema: (t, c, cc) =>
                {
                    t.Id = "default";
                    t.Contents.Add(
                        c.Type
                        .GetMethod(nameof(TestPage.GetData))
                        .GenerateRequiredSchema<Content>(cc.Drill("contents", t.Contents.Count))
                    );
                },
                order: Order.At.Override
            );

            conventions.AddMethodSchema(
                when: c => c.Type.Is<TestPage>() && c.Method.Name is nameof(TestPage.GetData),
                schema: () => B.Content(),
                order: Order.At.Override
            );
            conventions.AddMethodSchemaConfiguration<Content>(
                when: c => c.Type.Is<TestPage>() && c.Method.Name is nameof(TestPage.GetData),
                schema: tabContent => tabContent.Narrow = true,
                order: Order.At.Override
            );
            conventions.AddMethodComponent(
                when: c => c.Type.Is<TestPage>() && c.Method.Name is nameof(TestPage.GetData),
                where: cc => cc.Path.EndsWith("component"),
                component: (c, cc) => MethodText(c.Method, cc),
                order: Order.At.Override
            );
            conventions.AddMethodComponentConfiguration<Text>(
                when: c => c.Type.Is<TestPage>() && c.Method.Name is nameof(TestPage.GetData),
                component: t => t.Schema.MaxLength = 20,
                order: Order.At.Override
            );
        });
    }
}