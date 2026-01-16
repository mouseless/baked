using Baked.Architecture;
using Baked.Business;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Default.DomainComponents;

namespace Baked.Ux.ObjectWithListIsDataTable;

public class ObjectWithListIsDataTableUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeAttribute(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.Any(p =>
                        p.TryGet<DataAttribute>(out var data) &&
                        data.Visible &&
                        !p.PropertyType.Is<string>() &&
                        p.PropertyType.IsAssignableTo<IEnumerable>()
                    ),
                attribute: c => new ObjectWithListAttribute(
                    c.Type.GetMembers().Properties
                        .First(p =>
                            p.TryGet<DataAttribute>(out var data) &&
                            data.Visible &&
                            !p.PropertyType.Is<string>() &&
                            p.PropertyType.IsAssignableTo<IEnumerable>()
                        ).Name
                )
            );

            builder.Conventions.AddPropertyAttributeConfiguration<DataAttribute>(
                when: c =>
                    c.Type.TryGet<ObjectWithListAttribute>(out var objectWithList) &&
                    c.Property.Name == objectWithList.ListPropertyName,
                attribute: data => data.Visible = false
            );

            builder.Conventions.AddMethodComponent(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMetadata(out var returnMetadata) &&
                    returnMetadata.Has<ObjectWithListAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Content)),
                component: (c, cc) => MethodDataTable(c.Method, cc, options: dt =>
                {
                    dt.ItemsProp = c.Method.DefaultOverload
                        .ReturnType.SkipTask()
                        .GetMetadata()
                        .Get<ObjectWithListAttribute>()
                        .ListPropertyName
                        .Camelize();
                })
            );
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMetadata(out var returnMetadata) &&
                    returnMetadata.Has<ObjectWithListAttribute>(),
                component: (dt, c) =>
                {
                    dt.Schema.ItemsProp = c.Method.DefaultOverload
                        .ReturnType.SkipTask()
                        .GetMetadata()
                        .Get<ObjectWithListAttribute>()
                        .ListPropertyName
                        .Camelize();
                }
            );
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMembers(out var returnMembers) &&
                    returnMembers.TryGet<ObjectWithListAttribute>(out var objectWithList) &&
                    returnMembers
                        .Properties[objectWithList.ListPropertyName]
                        .PropertyType.TryGetElementType(out var elementType) &&
                    elementType.HasMembers(),
                component: (dt, c, cc) =>
                {
                    cc = cc.Drill(nameof(DataTable));

                    var returnMembers = c.Method.DefaultOverload.ReturnType.SkipTask().GetMembers();
                    var listPropertyName = returnMembers.Get<ObjectWithListAttribute>().ListPropertyName;
                    var elementType = returnMembers.Properties[listPropertyName].PropertyType.GetElementType();
                    var elementMembers = elementType.GetMembers();
                    foreach (var property in elementMembers.Properties.GetDataProperties())
                    {
                        var column = property.GetSchema<DataTable.Column>(cc.Drill(nameof(DataTable.Columns)));
                        if (column is null) { continue; }

                        dt.Schema.Columns.Add(column);
                    }

                    if (dt.Schema.DataKey is null && elementMembers.TryGetIdentifier(out var identifier))
                    {
                        dt.Schema.DataKey = identifier.RouteName;
                    }
                },
                order: -10
            );
            builder.Conventions.AddMethodSchema(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMembers(out var returnMembers) &&
                    returnMembers.TryGet<ObjectWithListAttribute>(out var objectWithList) &&
                    returnMembers
                        .Properties[objectWithList.ListPropertyName]
                        .PropertyType.TryGetElementType(out var elementType) &&
                    elementType.TryGetMembers(out var elementMembers) &&
                    elementMembers.Methods.Having<ActionAttribute>().Any(m => !m.Get<ActionAttribute>().HideInLists),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions)),
                schema: () => ActionsDataTableColumn()
            );
            builder.Conventions.AddMethodSchemaConfiguration<DataTable.Column>(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMembers(out var returnMembers) &&
                    returnMembers.TryGet<ObjectWithListAttribute>(out var objectWithList) &&
                    returnMembers
                        .Properties[objectWithList.ListPropertyName]
                        .PropertyType.TryGetElementType(out var elementType) &&
                    elementType.HasMembers(),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions)),
                schema: (col, c, cc) =>
                {
                    var returnMembers = c.Method.DefaultOverload.ReturnType.SkipTask().GetMembers();
                    var listPropertyName = returnMembers.Get<ObjectWithListAttribute>().ListPropertyName;
                    var elementType = returnMembers.Properties[listPropertyName].PropertyType.GetElementType();
                    var elementMembers = elementType.GetMembers();
                    foreach (var method in elementMembers.Methods.Having<ActionAttribute>())
                    {
                        if (method.Get<ActionAttribute>().HideInLists) { continue; }
                        if (method.Has<InitializerAttribute>()) { continue; }
                        if (method.GetAction().Method == HttpMethod.Get) { continue; }

                        var component = method.GetComponent(cc.Drill(method.Name));
                        if (component is null) { continue; }

                        col.Component += component;
                    }
                }
            );

            builder.Conventions.AddMethodSchema(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMetadata(out var returnMetadata) &&
                    returnMetadata.Has<ObjectWithListAttribute>(),
                schema: (c, cc) => MethodDataTableFooter(c.Method, cc)
            );
            builder.Conventions.AddMethodSchemaConfiguration<DataTable.Footer>(
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMembers(out var returnMembers) &&
                    returnMembers.Has<ObjectWithListAttribute>(),
                schema: (dtf, c, cc) =>
                {
                    var returnMembers = c.Method.DefaultOverload.ReturnType.SkipTask().GetMembers();
                    var listPropertyName = returnMembers.Get<ObjectWithListAttribute>().ListPropertyName;

                    foreach (var property in returnMembers.Properties.GetDataProperties())
                    {
                        if (property.Name == listPropertyName) { continue; }

                        property.Get<DataAttribute>().Label = null;

                        var column = property.GetSchema<DataTable.Column>(cc.Drill(nameof(DataTable.Columns)));
                        if (column is null) { continue; }

                        dtf.Columns.Add(column);
                    }
                }
            );

            builder.Conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                where: cc => cc.Path.Contains(nameof(DataTable), nameof(DataTable.FooterTemplate)),
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