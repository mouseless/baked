using Baked.Architecture;
using Baked.Test.Theme;

using B = Baked.Ui.Components;

namespace Baked.Test.Override.Ui;

public class DataTableSampleUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // DataTable fine tuning
            builder.Conventions.AddMethodComponentConfiguration<Baked.Ui.DataTable>(
                component: dt =>
                {
                    dt.Schema.ScrollHeight = "500px";
                    dt.Schema.Paginator = null;
                    dt.Schema.Rows = null;
                },
                when: c => c.Type.Is<DataTableSample>()
            );
            builder.Conventions.AddMethodSchemaConfiguration<Baked.Ui.DataTable.Export>(
                schema: dte =>
                {
                    dte.ParameterFormatter = null;
                    dte.ParameterSeparator = null;
                },
                when: c => c.Type.Is<DataTableSample>()
            );
            builder.Conventions.AddMethodSchema(
                schema: () => B.DataTableVirtualScroller(options: dtvs => dtvs.ItemSize = 45),
                when: c => c.Type.Is<DataTableSample>()
            );
        });
    }
}