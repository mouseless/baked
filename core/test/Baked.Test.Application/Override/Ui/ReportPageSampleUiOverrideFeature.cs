using Baked.Architecture;
using Baked.Test.Theme;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Ui.Datas;

using B = Baked.Ui.Components;

namespace Baked.Test.Override.Ui;

public class ReportPageSampleUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // Tabs
            builder.Conventions.AddMethodAttributeConfiguration<TabNameAttribute>(
                when: c => c.Type.Is<ReportPageSample>() && c.Method.DefaultOverload.ReturnType.SkipTask().Is<string>(),
                attribute: (tabName, c) => tabName.Value = "SingleValue"
            );
            builder.Conventions.AddMethodAttributeConfiguration<TabNameAttribute>(
                when: c => c.Type.Is<ReportPageSample>() && c.Method.DefaultOverload.ReturnsList(),
                attribute: (tabName, c) => tabName.Value = "DataTable"
            );
            builder.Conventions.AddTypeComponent(
                when: c => c.Type.Is<ReportPageSample>(),
                where: cc => cc.Path.EndsWith("SingleValue", nameof(Tab.Icon)),
                component: () => B.Icon("pi-box")
            );
            builder.Conventions.AddTypeComponent(
                when: c => c.Type.Is<ReportPageSample>(),
                where: cc => cc.Path.EndsWith("DataTable", nameof(Tab.Icon)),
                component: () => B.Icon("pi-table")
            );

            // Allowing admin token for report api
            builder.Conventions.AddMethodSchemaConfiguration<RemoteData>(
                when: c => c.Type.Is<ReportPageSample>(),
                schema: rd => rd.Headers = Inline(new { Authorization = "token-admin-ui" })
            );

            // Parameter overrides
            builder.Conventions.AddParameterComponent(
                when: c => c.Type.Is<ReportPageSample>() && c.Method.Name == nameof(ReportPageSample.With) && !c.Parameter.IsOptional,
                component: (c, cc) => EnumSelect(c.Parameter, cc)
            );
            builder.Conventions.AddParameterComponent(
                when: c => c.Type.Is<ReportPageSample>() && c.Method.Name == nameof(ReportPageSample.GetFirst) && c.Parameter.Name == "count",
                component: (c, cc) => EnumSelect(c.Parameter, cc)
            );

            // Page overrides
            builder.Conventions.AddTypeComponentConfiguration<TabbedPage>(
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