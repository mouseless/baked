using Baked.Business;
using Baked.Domain.Metadata;
using Baked.Orm;
using Baked.RestApi.Model;
using Baked.Theme.Default;

namespace Baked.Test.Domain;

public class SerializingMetadata
{
    static TypeMetadataModel ATypeMetadataModel(
        string? id = default,
        string? name = default,
        List<AttributeMetadataModel>? attributes = default,
        List<MethodMetadataModel>? methods = default,
        List<PropertyMetadataModel>? properties = default
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

    [Test]
    public void Serialize_given_type_metadata_model()
    {
        var expected = """
        SampleType @entity {
          @controller-model id="Baked.Test.Domain.SampleType" className="SampleType" groupName="Test"
        }
        """;
        var metadataType = ATypeMetadataModel(
            id: "Baked.Test.Domain.SampleType",
            name: "SampleType",
            attributes:
            [
                new(nameof(EntityAttribute)),
                new(nameof(ControllerModelAttribute),
                    ("Id", "Baked.Test.Domain.SampleType"),
                    ("ClassName", "SampleType"),
                    ("GroupName", "Test")
                )
            ]
        );
        var metadataSerializer = new KdlMetadataSerializer();

        var actual = metadataSerializer.Serialize(metadataType);

        actual.Trim().ShouldBe(expected.Trim());
    }

    [Test]
    public void Serializes_properties()
    {
        var expected = """
        SampleType {
          name @label {
            @data prop="Name"
          }
          surname @label
        }
        """;
        var metadataType = ATypeMetadataModel(
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
        var metadataSerializer = new KdlMetadataSerializer();

        var actual = metadataSerializer.Serialize(metadataType);

        actual.Trim().ShouldBe(expected.Trim());
    }

    [Test]
    public void Serializes_methods()
    {
        var expected = """
        SampleType {
          with @initializer {
            @action-model method="Post" routeParts="sample-types, id"
          }
        }
        """;
        var metadataType = ATypeMetadataModel(
            name: "SampleType",
            methods:
            [
                new("With",
                [
                    new(nameof(InitializerAttribute)),
                    new(nameof(ActionModelAttribute),
                    [
                        ("Method", "Post"),
                        ("RouteParts", new string[] { "sample-types", "id" })
                    ]),
                ]),
            ]
        );
        var metadataSerializer = new KdlMetadataSerializer();

        var actual = metadataSerializer.Serialize(metadataType);

        actual.Trim().ShouldBe(expected.Trim());
    }
}