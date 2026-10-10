using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using B = Baked.Ui.Components;

namespace Baked.Ux.ObjectWithListIsDataTable;

public class ObjectWithListIsDataTableUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.Any(p =>
                        p.TryGet<DataAttribute>(out var data) &&
                        data.Visible &&
                        !p.PropertyType.Is<string>() &&
                        p.PropertyType.IsAssignableTo<IEnumerable>()
                    ),
                attribute: c => new ObjectWithList(
                    c.Type.GetMembers().Properties
                        .First(p =>
                            p.TryGet<DataAttribute>(out var data) &&
                            data.Visible &&
                            !p.PropertyType.Is<string>() &&
                            p.PropertyType.IsAssignableTo<IEnumerable>()
                        ).Name
                ),
                order: Order.At.Infra
            );

            conventions.EditPropertyAttribute<DataAttribute>(
                when: c =>
                    c.Type.TryGet<ObjectWithList>(out var objectWithList) &&
                    c.Property.Name == objectWithList.ListPropertyName,
                attribute: data => data.Visible = false,
                order: Order.At.Infra
            );

            conventions.AddMethodComponent(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMetadata(out var returnMetadata) &&
                    returnMetadata.Has<ObjectWithList>(),
                where: cc => cc.Path.EndsWith("data-panel", "content"),
                component: () => B.DataTable()
            );
            conventions.EditMethodComponent<DataTable>(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMetadata(out var returnMetadata) &&
                    returnMetadata.Has<ObjectWithList>(),
                component: (dt, c) =>
                {
                    dt.Schema.ItemsProp = c.Method.DefaultOverload
                        .ReturnType.SkipTask()
                        .GetMetadata()
                        .Get<ObjectWithList>()
                        .ListPropertyName
                        .Camelize();
                }
            );
            conventions.EditMethodComponent<DataTable>(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMembers(out var returnMembers) &&
                    returnMembers.TryGet<ObjectWithList>(out var objectWithList) &&
                    returnMembers
                        .Properties[objectWithList.ListPropertyName]
                        .PropertyType.TryGetElementType(out var elementType) &&
                    elementType.HasMembers(),
                component: (dt, c, cc) =>
                {
                    cc = cc.Drill("data-table");

                    var returnMembers = c.Method.DefaultOverload.ReturnType.SkipTask().GetMembers();
                    var listPropertyName = returnMembers.Get<ObjectWithList>().ListPropertyName;
                    var elementType = returnMembers.Properties[listPropertyName].PropertyType.GetElementType();
                    var elementMembers = elementType.GetMembers();
                    foreach (var property in elementMembers.Properties.GetDataProperties())
                    {
                        var column = property.GenerateSchema<DataTable.Column>(cc.Drill("columns"));
                        if (column is null) { continue; }

                        dt.Schema.Columns.Add(column);
                    }

                    if (dt.Schema.DataKey is null && elementMembers.TryGetIdInfo(out var idInfo))
                    {
                        dt.Schema.DataKey = idInfo.RouteName;
                    }
                },
                order: -10
            );
            conventions.AddMethodSchema(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMembers(out var returnMembers) &&
                    returnMembers.TryGet<ObjectWithList>(out var objectWithList) &&
                    returnMembers
                        .Properties[objectWithList.ListPropertyName]
                        .PropertyType.TryGetElementType(out var elementType) &&
                    elementType.TryGetMembers(out var elementMembers) &&
                    elementMembers.Methods.Having<UiAction>().Any(m => !m.Get<UiAction>().HideInLists),
                where: cc => cc.Path.EndsWith("data-table", "actions"),
                schema: () => B.DataTableColumn()
            );
            conventions.EditMethodSchema<DataTable.Column>(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMembers(out var returnMembers) &&
                    returnMembers.TryGet<ObjectWithList>(out var objectWithList) &&
                    returnMembers
                        .Properties[objectWithList.ListPropertyName]
                        .PropertyType.TryGetElementType(out var elementType) &&
                    elementType.HasMembers(),
                where: cc => cc.Path.EndsWith("data-table", "actions"),
                schema: (col, c, cc) =>
                {
                    var returnMembers = c.Method.DefaultOverload.ReturnType.SkipTask().GetMembers();
                    var listPropertyName = returnMembers.Get<ObjectWithList>().ListPropertyName;
                    var elementType = returnMembers.Properties[listPropertyName].PropertyType.GetElementType();
                    var elementMembers = elementType.GetMembers();
                    foreach (var method in elementMembers.Methods.Having<UiAction>())
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

            conventions.AddMethodSchema(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMetadata(out var returnMetadata) &&
                    returnMetadata.Has<ObjectWithList>(),
                schema: () => B.DataTableFooter()
            );
            conventions.EditMethodSchema<DataTable.Footer>(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMembers(out var returnMembers) &&
                    returnMembers.Has<ObjectWithList>(),
                schema: (dtf, c, cc) =>
                {
                    var returnMembers = c.Method.DefaultOverload.ReturnType.SkipTask().GetMembers();
                    var listPropertyName = returnMembers.Get<ObjectWithList>().ListPropertyName;

                    foreach (var property in returnMembers.Properties.GetDataProperties())
                    {
                        if (property.Name == listPropertyName) { continue; }

                        property.Get<DataAttribute>().Label = null;

                        var column = property.GenerateSchema<DataTable.Column>(cc.Drill("columns"));
                        if (column is null) { continue; }

                        dtf.Columns.Add(column);
                    }
                }
            );

            conventions.EditPropertySchema<DataTable.Column>(
                where: cc => cc.Path.Contains("data-table", "footer-template"),
                schema: dtc =>
                {
                    dtc.Title = null;
                    dtc.Exportable = null;
                },
                order: 10
            );
        });
    }
}