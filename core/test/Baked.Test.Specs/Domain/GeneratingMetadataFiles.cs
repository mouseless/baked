using Baked.Business;
using Baked.Domain.Metadata;
using Baked.Orm;
using Baked.RestApi.Model;
using Baked.Theme.Default;

namespace Baked.Test.Domain;

public class GeneratingMetadataFiles : TestSpec
{
    //[Entity]
    //[ControllerModel(ClassName = nameof(SampleType), GroupName = "Test")]
    //public class SampleType
    //{
    //    [Data("id")]
    //    [Id("id")]
    //    public Id Id { get; set; }
    //    [Data("name")]
    //    [Label]
    //    public string Name { get; set; } = default!;

    //    [Initializer]
    //    [ActionModel(method: "Post")]
    //    public SampleType With(
    //        [ParameterModel] string name
    //    )
    //    {
    //        Name = name;

    //        return this;
    //    }

    //    [ActionModel]
    //    public void Update() { }
    //    internal void Delete() { }
    //}

    [Test]
    public void Generates_given_metadata()
    {
        var expected = """
        SampleType @entity {
          @controller-model id="Baked.Test.Domain.SampleType" className="SampleType" groupName="Test"
          name @label {
            @data prop="Name"
          }
        }
        """;
        var metadataType = new TypeMetadataModel()
        {
            Name = "SampleType",
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
                ])
            ]
        };
        var metadataSerializer = new MetadataSerializer();

        var actual = metadataSerializer.Serialize(metadataType);

        actual.Trim().ShouldBe(expected.Trim());
    }
}