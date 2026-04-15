using Baked.Business;
using Baked.Domain.Export;
using Baked.Orm;
using Baked.RestApi.Model;
using Baked.Theme.Default;

namespace Baked.Test.Domain;

public class GeneratingAttributeExportFiles : TestSpec
{
    static TypeAttributeExportModel ATypeExportModel(
        string? id = default,
        string? name = default,
        string? groupName = default,
        List<AttributeExportModel>? attributes = default,
        List<MethodAttributeExportModel>? methods = default,
        List<PropertyAttributeExportModel>? properties = default
    )
    {
        name ??= "Test";
        id ??= $"Baked.Domain.Test.{name}";
        groupName ??= name;

        return new(id, name)
        {
            GroupName = groupName,
            Attributes = attributes ?? [],
            Methods = methods ?? [],
            Properties = properties ?? []
        };
    }

    class FakeAttribute : Attribute;

    [Test]
    public void Serialize_given_type_metadata_model()
    {
        var expected = """
        sample-type @entity {
          @fake camelCase="CamelCase" string="Post" array="System.String[]" boolTrue=#true int=1
          name @label {
            @data prop="Name"
          }
          surname @label
          method-name @initializer {
            @action-model method="Post" routeParts="System.String[]"
            id {
              @parameter-model from="Route"
            }
          }
        }
        """;
        var typeExport = ATypeExportModel(
            id: "Baked.Test.Domain.SampleType",
            name: "SampleType",
            attributes:
            [
                new(nameof(EntityAttribute)),
                new(nameof(FakeAttribute),
                    ("CamelCase", "CamelCase"),
                    ("String", "Post"),
                    ("Array", new[] { "sample-types", "id" }),
                    ("Dictionary", new Dictionary<string, object>{ {"id", new { } } }),
                    ("BoolTrue", true),
                    ("BoolFalse", false),
                    ("Int", 1),
                    ("ValueNull", null)
                )
            ],
            methods:
            [
                new("MethodName",
                [
                    new(nameof(InitializerAttribute)),
                    new(nameof(ActionModelAttribute), ("Method", "Post"), ("RouteParts", new[] { "sample-types", "id" })),
                ])
                {
                    Parameters = [new("Id", [new(nameof(ParameterModelAttribute), ("From", ParameterModelFrom.Route))])]
                },
            ],
            properties:
            [
                new("Name", [new(nameof(DataAttribute), ("Prop","Name")), new(nameof(LabelAttribute))]),
                new("Surname", [new(nameof(LabelAttribute))])
            ]
        );
        var exportSet = new AttributeExportSetModel(new(new[] { typeExport }));
        var contentGenerator = new AttributeExportFileContentGenerator(new KdlTypeExportSerializer(), type => type.GroupName);

        var actual = contentGenerator.Generate(exportSet);

        actual["SampleType"].Trim().ShouldBe(expected.Trim());
    }

    [Test]
    public void Return_empty_content_with_comment_for_no_attribute_export_data()
    {
        var expected = """
        // No exportable data exists for 'Baked.Domain.Test.SampleTypeA'

        // No exportable data exists for 'Baked.Domain.Test.SampleTypeB'

        sample-type-c @entity
        """;
        var typeExportA = ATypeExportModel(
            id: "Baked.Domain.Test.SampleTypeA",
            name: "SampleTypeA",
            groupName: "Test"
        );
        var typeExportB = ATypeExportModel(
            id: "Baked.Domain.Test.SampleTypeB",
            name: "SampleTypeB",
            groupName: "Test"
        );
        var typeExportC = ATypeExportModel(
           id: "Baked.Domain.Test.SampleTypeC",
           name: "SampleTypeC",
           groupName: "Test",
           attributes: [new(nameof(EntityAttribute))]
       );

        var exportSet = new AttributeExportSetModel(new(new[] { typeExportA, typeExportB, typeExportC }));
        var contentGenerator = new AttributeExportFileContentGenerator(new KdlTypeExportSerializer(), type => type.GroupName);

        var actual = contentGenerator.Generate(exportSet);

        actual["Test"].Trim().ShouldBe(expected.Trim());
    }
}