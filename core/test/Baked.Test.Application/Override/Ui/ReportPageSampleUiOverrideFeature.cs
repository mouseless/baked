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
                attribute: (tabName, c) => tabName.Value = "SingleValue",
                when: c => c.Type.Is<ReportPageSample>() && c.Method.DefaultOverload.ReturnType.SkipTask().Is<string>()
            );
            builder.Conventions.AddMethodAttributeConfiguration<TabNameAttribute>(
                attribute: (tabName, c) => tabName.Value = "DataTable",
                when: c => c.Type.Is<ReportPageSample>() && c.Method.DefaultOverload.ReturnsList()
            );
            builder.Conventions.AddTypeComponent(
                component: () => B.Icon("pi-box"),
                when: c => c.Type.Is<ReportPageSample>(),
                where: cc => cc.Path.EndsWith("SingleValue", nameof(ReportPage.Tab.Icon))
            );
            builder.Conventions.AddTypeComponent(
                component: () => B.Icon("pi-table"),
                when: c => c.Type.Is<ReportPageSample>(),
                where: cc => cc.Path.EndsWith("DataTable", nameof(ReportPage.Tab.Icon))
            );

            // Allowing admin token for report api
            builder.Conventions.AddMethodSchemaConfiguration<RemoteData>(
                schema: rd => rd.Headers = Inline(new { Authorization = "token-admin-ui" }),
                when: c => c.Type.Is<ReportPageSample>()
            );

            // Parameter overrides
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelect(c.Parameter, cc),
                when: c => c.Type.Is<ReportPageSample>() && c.Method.Name == nameof(ReportPageSample.With) && !c.Parameter.IsOptional
            );
            builder.Conventions.AddParameterComponent(
                component: (c, cc) => EnumSelect(c.Parameter, cc),
                when: c => c.Type.Is<ReportPageSample>() && c.Method.Name == nameof(ReportPageSample.GetFirst) && c.Parameter.Name == "count"
            );

            // Page overrides
            builder.Conventions.AddTypeComponentConfiguration<ReportPage>(
                component: rp =>
                {
                    rp.Schema.Inputs.Single(p => p.Name == "required").Default = null;
                    rp.Schema.Tabs[0].Contents[1].Narrow = true;
                    rp.Schema.Tabs[0].Contents[2].Narrow = true;
                    rp.Schema.Tabs[0].Contents[1].Component.Schema.As<DataPanel>().Collapsed = true;
                    rp.Schema.Tabs[0].Contents[2].Component.Schema.As<DataPanel>().Collapsed = true;
                    rp.Schema.Tabs[1].Contents[1].Component.Schema.As<DataPanel>().Collapsed = true;
                },
                when: c => c.Type.Is<ReportPageSample>()
            );
        });
    }
}