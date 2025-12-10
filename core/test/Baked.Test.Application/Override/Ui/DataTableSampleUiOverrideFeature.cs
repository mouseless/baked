using Baked.Architecture;
using Baked.Test.Theme;
using Baked.Ui;

using B = Baked.Ui.Components;

namespace Baked.Test.Override.Ui;

public class DataTableSampleUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // DataTable fine tuning
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                when: c => c.Type.Is<DataTableSample>(),
                component: dt =>
                {
                    dt.Schema.ScrollHeight = "500px";
                    dt.Schema.Paginator = null;
                    dt.Schema.Rows = null;
                }
            );
            builder.Conventions.AddMethodSchemaConfiguration<DataTable.Export>(
                when: c => c.Type.Is<DataTableSample>(),
                schema: dte =>
                {
                    dte.ParameterFormatter = null;
                    dte.ParameterSeparator = null;
                }
            );
            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<DataTableSample>(),
                schema: () => B.DataTableVirtualScroller(options: dtvs => dtvs.ItemSize = 45)
            );
        });
    }
}