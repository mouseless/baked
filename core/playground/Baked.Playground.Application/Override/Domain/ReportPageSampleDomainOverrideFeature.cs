using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Playground.Theme;
using Baked.Ui;

using static Baked.Ui.Datas;

using B = Baked.Ui.Components;

namespace Baked.Playground.Override.Domain;

public class ReportPageSampleDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            // Tabs
            conventions.EditMethodAttribute<GroupAttribute>(
                when: c => c.Type.Is<ReportPageSample>() && c.Method.DefaultOverload.ReturnType.SkipTask().Is<string>(),
                attribute: group => group.TabName = "single-value",
                order: Order.At.Override
            );
            conventions.EditMethodAttribute<GroupAttribute>(
                when: c => c.Type.Is<ReportPageSample>() && c.Method.DefaultOverload.ReturnsList(),
                attribute: group => group.TabName = "data-table",
                order: Order.At.Override
            );
            conventions.AddTypeComponent(
                when: c => c.Type.Is<ReportPageSample>(),
                where: cc => cc.Path.EndsWith("single-value", "icon"),
                component: () => B.Icon("pi-box"),
                order: Order.At.Override
            );
            conventions.AddTypeComponent(
                when: c => c.Type.Is<ReportPageSample>(),
                where: cc => cc.Path.EndsWith("data-table", "icon"),
                component: () => B.Icon("pi-table"),
                order: Order.At.Override
            );

            // Allowing admin token for report api
            conventions.EditMethodSchema<RemoteData>(
                when: c => c.Type.Is<ReportPageSample>(),
                schema: rd => rd.Headers = Inline(new { Authorization = "token-admin-ui" }),
                order: Order.At.Override
            );

            // Parameter overrides
            conventions.AddParameterComponent(
                when: c => c.Type.Is<ReportPageSample>() && c.Method.Name is nameof(ReportPageSample.With) && !c.Parameter.IsNullable,
                component: () => B.Select(),
                order: Order.At.Override
            );
            conventions.AddParameterComponent(
                when: c => c.Type.Is<ReportPageSample>() && c.Method.Name is nameof(ReportPageSample.GetFirst) && c.Parameter.Name is "count",
                component: () => B.Select(),
                order: Order.At.Override
            );

            // Page overrides
            conventions.EditTypeComponent<TabbedPage>(
                when: c => c.Type.Is<ReportPageSample>(),
                component: tp =>
                {
                    tp.Schema.Inputs.Single(p => p.Name == "required").Default = null;
                    tp.Schema.Tabs[0].Contents[1].Narrow = true;
                    tp.Schema.Tabs[0].Contents[2].Narrow = true;
                    tp.Schema.Tabs[0].Contents[1].Component.Schema.As<DataPanel>().Collapsed = true;
                    tp.Schema.Tabs[0].Contents[2].Component.Schema.As<DataPanel>().Collapsed = true;
                    tp.Schema.Tabs[1].Contents[1].Component.Schema.As<DataPanel>().Collapsed = true;
                },
                order: Order.At.Override
            );
        });
    }
}