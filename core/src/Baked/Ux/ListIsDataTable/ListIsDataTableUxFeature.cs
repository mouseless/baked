using Baked.Architecture;
using Baked.Business;
using Baked.Theme.Default;
using Baked.Ui;

using B = Baked.Ui.Components;

namespace Baked.Ux.ListIsDataTable;

public class ListIsDataTableUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddMethodComponent(
                when: c => c.Method.DefaultOverload.ReturnsList(),
                where: cc => cc.Path.EndsWith("*-panel", "content") || cc.Path.EndsWith("*-container", "content"),
                component: () => B.DataTable()
            );
            conventions.EditMethodComponent<DataTable>(
                when: c =>
                    c.Method.DefaultOverload.ReturnsList() &&
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetElementType(out var elementType) &&
                    elementType.HasMembers(),
                component: (dt, c, cc) =>
                {
                    cc = cc.Drill("data-table");

                    var members = c.Method.DefaultOverload.ReturnType.SkipTask().GetElementType().GetMembers();
                    foreach (var property in members.Properties.GetDataProperties())
                    {
                        var column = property.GenerateSchema<DataTable.Column>(cc.Drill("columns"));
                        if (column is null) { continue; }

                        dt.Schema.Columns.Add(column);
                    }

                    if (dt.Schema.DataKey is null && members.TryGetIdInfo(out var idInfo))
                    {
                        dt.Schema.DataKey = idInfo.RouteName;
                    }
                },
                order: -10
            );
            conventions.AddMethodSchema(
                when: c =>
                    c.Method.DefaultOverload.ReturnsList() &&
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetElementType(out var elementType) &&
                    elementType.TryGetMembers(out var elementMembers) &&
                    elementMembers.Methods.Having<UiAction>().Any(m => !m.Get<UiAction>().HideInLists),
                where: cc => cc.Path.EndsWith("data-table", "actions"),
                schema: () => B.DataTableColumn()
            );
            conventions.EditMethodSchema<DataTable.Column>(
                when: c =>
                    c.Method.DefaultOverload.ReturnsList() &&
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetElementType(out var itemType) &&
                    itemType.HasMembers(),
                where: cc => cc.Path.EndsWith("data-table", "actions"),
                schema: (col, c, cc) =>
                {
                    var itemMembers = c.Method.DefaultOverload.ReturnType.SkipTask().GetElementType().GetMembers();
                    foreach (var method in itemMembers.Methods.Having<UiAction>())
                    {
                        if (method.Get<UiAction>().HideInLists) { continue; }
                        if (method.Has<Initializer>()) { continue; }
                        if (method.GetAction().Method == HttpMethod.Get) { continue; }

                        var component = method.GenerateComponent(cc.Drill(method.Name));
                        if (component is null) { continue; }

                        col.Component += component;
                    }
                }
            );
        });
    }
}