using Baked.Business;
using Baked.Domain.Metadata;
using Baked.Orm;
using Baked.RestApi.Model;
using Baked.Theme.Default;

namespace Baked.Test.Domain;

public class GeneratingMetadataFiles
{
    [Test]
    public void Generates_given_metadata()
    {
        var expected = """
        SampleType @entity {
          @controller-model id="Baked.Test.Domain.SampleType" className="SampleType" groupName="Test"
          name @label {
            @data prop="Name"
          }
          surname @label
        }
        """;
        var metadataType = new TypeMetadataModel("Baked.Test.Domain.SampleType", "SampleType")
        {
            Attributes =
            [
                new(nameof(EntityAttribute)),
                new(nameof(ControllerModelAttribute),
                    ("Id", "Baked.Test.Domain.SampleType"),
                    ("ClassName", "SampleType"),
                    ("GroupName", "Test")
                )
            ],
            Properties =
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
        };
        var metadataSerializer = new KdlMetadataSerializer();

        var actual = metadataSerializer.Serialize(metadataType);

        actual.Trim().ShouldBe(expected.Trim());
    }
}