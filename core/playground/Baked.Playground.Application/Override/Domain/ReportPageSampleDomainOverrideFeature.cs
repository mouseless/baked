using Baked.Architecture;
using Baked.Business;
using Baked.Playground.Theme;
using Baked.Ui;

using static Baked.Theme.Default.DomainComponents;
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
            conventions.AddMethodAttributeConfiguration<GroupAttribute>(
                when: c => c.Type.Is<ReportPageSample>() && c.Method.DefaultOverload.ReturnType.SkipTask().Is<string>(),
                attribute: group => group.TabName = "SingleValue"
            );
            conventions.AddMethodAttributeConfiguration<GroupAttribute>(
                when: c => c.Type.Is<ReportPageSample>() && c.Method.DefaultOverload.ReturnsList(),
                attribute: group => group.TabName = "DataTable"
            );
            conventions.AddTypeComponent(
                when: c => c.Type.Is<ReportPageSample>(),
                where: cc => cc.Path.EndsWith("SingleValue", nameof(Tab.Icon)),
                component: () => B.Icon("pi-box")
            );
            conventions.AddTypeComponent(
                when: c => c.Type.Is<ReportPageSample>(),
                where: cc => cc.Path.EndsWith("DataTable", nameof(Tab.Icon)),
                component: () => B.Icon("pi-table")
            );

            // Allowing admin token for report api
            conventions.AddMethodSchemaConfiguration<RemoteData>(
                when: c => c.Type.Is<ReportPageSample>(),
                schema: rd => rd.Headers = Inline(new { Authorization = "token-admin-ui" })
            );

            // Parameter overrides
            conventions.AddParameterComponent(
                when: c => c.Type.Is<ReportPageSample>() && c.Method.Name == nameof(ReportPageSample.With) && !c.Parameter.IsNullable,
                component: (c, cc) => ParameterSelect(c.Parameter, cc)
            );
            conventions.AddParameterComponent(
                when: c => c.Type.Is<ReportPageSample>() && c.Method.Name == nameof(ReportPageSample.GetFirst) && c.Parameter.Name == "count",
                component: (c, cc) => ParameterSelect(c.Parameter, cc)
            );

            // Page overrides
            conventions.AddTypeComponentConfiguration<TabbedPage>(
                when: c => c.Type.Is<ReportPageSample>(),
                component: tp =>
                {
                    tp.Schema.Inputs.Single(p => p.Name == "required").Default = null;
                    tp.Schema.Tabs[0].Contents[1].Narrow = true;
                    tp.Schema.Tabs[0].Contents[2].Narrow = true;
                    tp.Schema.Tabs[0].Contents[1].Component.Schema.As<DataPanel>().Collapsed = true;
                    tp.Schema.Tabs[0].Contents[2].Component.Schema.As<DataPanel>().Collapsed = true;
                    tp.Schema.Tabs[1].Contents[1].Component.Schema.As<DataPanel>().Collapsed = true;
                }
            );
        });
    }
}