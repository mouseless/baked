using Baked.Business;
using Baked.Domain.Export;
using Baked.Orm;
using Baked.RestApi.Model;
using Baked.Theme.Default;

namespace Baked.Test.Domain;

public class SerializingAttributeExports : TestSpec
{
    static TypeExportModel ATypeExportModel(
        string? id = default,
        string? name = default,
        List<AttributeExportModel>? attributes = default,
        List<MethodExportModel>? methods = default,
        List<PropertyExportModel>? properties = default
    )
    {
        id ??= "Baked.Domain.Test";
        name ??= "Test";

        return new(id, name)
        {
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
          @fake className="SampleType" string="Post" array="System.String[]" bool=#false int=1
        }
        """;
        var metadataType = ATypeExportModel(
            id: "Baked.Test.Domain.SampleType",
            name: "SampleType",
            attributes:
            [
                new(nameof(EntityAttribute)),
                new(nameof(FakeAttribute),
                [
                    ("ClassName", "SampleType"),
                    ("String", "Post"),
                    ("Array", new[] { "sample-types", "id" }),
                    ("Dictionary", new Dictionary<string, object>{ {"id", new { } } }),
                    ("Bool", false),
                    ("Int", 1)
                ])
            ]
        );
        var metadataSerializer = new KdlTypeExportSerializer();

        var actual = metadataSerializer.Serialize(metadataType);

        actual.Trim().ShouldBe(expected.Trim());
    }

    [Test]
    public void Serializes_properties()
    {
        var expected = """
        sample-type {
          name @label {
            @data prop="Name"
          }
          surname @label
        }
        """;
        var metadataType = ATypeExportModel(
            name: "SampleType",
            properties:
            [
                new("Name",
                [
                    new(nameof(DataAttribute), ("Prop","Name")),
                    new(nameof(LabelAttribute))
                ]),
                new("Surname",
                [
                    new(nameof(LabelAttribute))
                ])
            ]
        );
        var metadataSerializer = new KdlTypeExportSerializer();

        var actual = metadataSerializer.Serialize(metadataType);

        actual.Trim().ShouldBe(expected.Trim());
    }

    [Test]
    public void Serializes_methods()
    {
        var expected = """
        sample-type {
          @locatable isAsync=#false

          with @initializer {
            @action-model method="Post" routeParts="System.String[]"
          }
        }
        """;
        var metadataType = ATypeExportModel(
            name: "SampleType",
            methods:
            [
                new("With",
                [
                    new(nameof(LocatableAttribute),
                        new(nameof(LocatableAttribute.QueryType), null),
                        new(nameof(LocatableAttribute.IsAsync), false)
                    ),
                    new(nameof(InitializerAttribute)),
                    new(nameof(ActionModelAttribute),
                    [
                        ("Method", "Post"),
                        ("RouteParts", new[] { "sample-types", "id" }),
                    ]),
                ]),
            ]
        );
        var metadataSerializer = new KdlTypeExportSerializer();

        var actual = metadataSerializer.Serialize(metadataType);

        actual.Trim().ShouldBe(expected.Trim());
    }

    [Test]
    public void Return_empty_content_with_comment_for_no_attribute_export_data() =>
        this.ShouldFail();
}