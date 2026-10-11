using Baked.Architecture;
using Baked.Domain.Configuration;
using Baked.Playground.Theme;
using Baked.Ui;

namespace Baked.Playground.Override.Domain;

public class TestPageDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddTypeComponent<TabbedPage>(
                when: c => c.Type.Is<TestPage>(),
                where: cc => cc.Path.EndsWith("page"),
                order: Order.At.Override
            );
            conventions.EditTypeComponent<TabbedPage>(
                when: c => c.Type.Is<TestPage>(),
                component: (tp, c, cc) =>
                {
                    tp.Schema.Path = "test-page";
                    tp.Schema.Title?.Data = Fetch.Inline("Test Page");
                    tp.Schema.Tabs.Add(
                        c.Type.GenerateRequiredSchema<Tab>(cc.Drill("tabs", "default"))
                    );
                },
                order: Order.At.Override
            );
            conventions.AddTypeSchema<Tab>(
                when: c => c.Type.Is<TestPage>(),
                where: cc => cc.Path.EndsWith("tabs", "default"),
                order: Order.At.Override
            );
            conventions.EditTypeSchema<Tab>(
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

            conventions.AddMethodSchema<Content>(
                when: c => c.Type.Is<TestPage>() && c.Method.Name is nameof(TestPage.GetData),
                order: Order.At.Override
            );
            conventions.EditMethodSchema<Content>(
                when: c => c.Type.Is<TestPage>() && c.Method.Name is nameof(TestPage.GetData),
                schema: tabContent => tabContent.Narrow = true,
                order: Order.At.Override
            );
            conventions.AddMethodComponent<Text>(
                when: c => c.Type.Is<TestPage>() && c.Method.Name is nameof(TestPage.GetData),
                where: cc => cc.Path.EndsWith("component"),
                order: Order.At.Override
            );
            conventions.EditMethodComponent<Text>(
                when: c => c.Type.Is<TestPage>() && c.Method.Name is nameof(TestPage.GetData),
                component: t => t.Schema.MaxLength = 20,
                order: Order.At.Override
            );
        });
    }
}