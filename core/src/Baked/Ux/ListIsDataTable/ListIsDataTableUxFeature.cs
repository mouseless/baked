using Baked.Architecture;
using Baked.Theme.Default;
using Baked.Ui;

using static Baked.Theme.Default.DomainComponents;

using B = Baked.Ui.Components;

namespace Baked.Ux.ListIsDataTable;

public class ListIsDataTableUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddMethodComponent(
                when: c => c.Method.DefaultOverload.ReturnsList(),
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Content)),
                component: (c, cc) => MethodDataTable(c.Method, cc)
            );
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                when: c =>
                    c.Method.DefaultOverload.ReturnsList() &&
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetElementType(out var elementType) &&
                    elementType.HasMembers(),
                component: (dt, c, cc) =>
                {
                    cc = cc.Drill(nameof(DataTable));

                    var members = c.Method.DefaultOverload.ReturnType.SkipTask().GetElementType().GetMembers();
                    foreach (var property in members.Properties.GetDataProperties())
                    {
                        var column = property.GetSchema<DataTable.Column>(cc.Drill(nameof(DataTable.Columns)));
                        if (column is null) { continue; }

                        dt.Schema.Columns.Add(column);
                    }

                    if (dt.Schema.DataKey is null && members.Properties.Having<IdAttribute>().Any())
                    {
                        dt.Schema.DataKey = members.Properties.Having<IdAttribute>().Single().Get<DataAttribute>().Prop;
                    }
                },
                order: -10
            );
            builder.Conventions.AddMethodSchema(
                when: c =>
                    c.Method.DefaultOverload.ReturnsList() &&
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetElementType(out var elementType) &&
                    elementType.TryGetMembers(out var elementMembers) &&
                    elementMembers.Methods.Having<ActionAttribute>().Any(m => !m.Get<ActionAttribute>().HideInLists),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions)),
                schema: (c, cc) => B.DataTableColumn(nameof(DataTable.Actions))
            );
        });
    }
}