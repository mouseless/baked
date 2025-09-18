using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Theme.Default;

using static Baked.Theme.Default.DomainComponents;

namespace Baked.Ux.ListIsDataTable;

public class ListIsDataTableUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddMethodComponent(
                component: (c, cc) => MethodDataTable(c.Method, cc),
                whenMethod: c => c.Method.Has<ActionModelAttribute>() && c.Method.DefaultOverload.ReturnsList(),
                whenComponent: c => c.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Content))
            );
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                component: (dt, c, cc) =>
                {
                    var members = c.Method.DefaultOverload.ReturnType.SkipTask().GetElementType().GetMembers();
                    foreach (var property in members.Properties.GetDataProperties())
                    {
                        var column = property.GetSchema<DataTable.Column>(cc.Drill(nameof(DataTable.Columns)));
                        if (column is null) { continue; }

                        dt.Schema.Columns.Add(column);
                    }
                },
                whenMethod: c =>
                    c.Method.DefaultOverload.ReturnsList() &&
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetElementType(out var elementType) &&
                    elementType.HasMembers()
            );
        });
    }
}