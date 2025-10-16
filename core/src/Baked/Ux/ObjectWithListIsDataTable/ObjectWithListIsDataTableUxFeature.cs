using Baked.Architecture;
using Baked.RestApi.Model;
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
                attribute: c => new ObjectWithListAttribute(
                    c.Type.GetMembers().Properties
                        .First(p =>
                            p.TryGet<DataAttribute>(out var data) &&
                            data.Visible &&
                            !p.PropertyType.Is<string>() &&
                            p.PropertyType.IsAssignableTo<IEnumerable>()
                        ).Name
                ),
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Properties.Any(p =>
                        p.TryGet<DataAttribute>(out var data) &&
                        data.Visible &&
                        !p.PropertyType.Is<string>() &&
                        p.PropertyType.IsAssignableTo<IEnumerable>()
                    )
            );

            builder.Conventions.AddPropertyAttributeConfiguration<DataAttribute>(
                attribute: data => data.Visible = false,
                when: c =>
                    c.Type.TryGet<ObjectWithListAttribute>(out var objectWithList) &&
                    c.Property.Name == objectWithList.ListPropertyName
            );

            builder.Conventions.AddMethodComponent(
                component: (c, cc) => MethodDataTable(c.Method, cc, options: dt =>
                {
                    dt.ItemsProp = c.Method.DefaultOverload
                        .ReturnType.SkipTask()
                        .GetMetadata()
                        .Get<ObjectWithListAttribute>()
                        .ListPropertyName
                        .Camelize();
                }),
                when: c =>
                    c.Method.Has<ActionModelAttribute>() &&
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMetadata(out var returnMetadata) &&
                    returnMetadata.Has<ObjectWithListAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Content))
            );
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                component: (dt, c) =>
                {
                    dt.Schema.ItemsProp = c.Method.DefaultOverload
                        .ReturnType.SkipTask()
                        .GetMetadata()
                        .Get<ObjectWithListAttribute>()
                        .ListPropertyName
                        .Camelize();
                },
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMetadata(out var returnMetadata) &&
                    returnMetadata.Has<ObjectWithListAttribute>()
            );
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                component: (dt, c, cc) =>
                {
                    cc = cc.Drill(nameof(DataTable));

                    var returnMembers = c.Method.DefaultOverload.ReturnType.SkipTask().GetMembers();
                    var listPropertyName = returnMembers.Get<ObjectWithListAttribute>().ListPropertyName;
                    var elementType = returnMembers.Properties[listPropertyName].PropertyType.GetElementType();

                    var members = elementType.GetMembers();
                    foreach (var property in members.Properties.GetDataProperties())
                    {
                        var column = property.GetSchema<DataTable.Column>(cc.Drill(nameof(DataTable.Columns)));
                        if (column is null) { continue; }

                        dt.Schema.Columns.Add(column);
                    }
                },
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMembers(out var returnMembers) &&
                    returnMembers.TryGet<ObjectWithListAttribute>(out var objectWithList) &&
                    returnMembers
                        .Properties[objectWithList.ListPropertyName]
                        .PropertyType.TryGetElementType(out var elementType) &&
                    elementType.HasMembers(),
                order: -10
            );

            builder.Conventions.AddMethodSchema(
                schema: (c, cc) => MethodDataTableFooter(c.Method, cc),
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMetadata(out var returnMetadata) &&
                    returnMetadata.Has<ObjectWithListAttribute>()
            );
            builder.Conventions.AddMethodSchemaConfiguration<DataTable.Footer>(
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
                },
                when: c =>
                    c.Method.DefaultOverload.ReturnType.SkipTask().TryGetMembers(out var returnMembers) &&
                    returnMembers.Has<ObjectWithListAttribute>()
            );

            builder.Conventions.AddPropertySchemaConfiguration<DataTable.Column>(
                schema: dtc =>
                {
                    dtc.Title = null;
                    dtc.Exportable = null;
                },
                where: cc => cc.Path.Contains(nameof(DataTable), nameof(DataTable.FooterTemplate)),
                order: 10
            );
        });
    }
}