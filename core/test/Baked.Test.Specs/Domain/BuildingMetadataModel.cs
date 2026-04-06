using Baked.Domain.Metadata;
using Baked.Orm;
using Baked.Playground.Orm;
using Baked.RestApi.Model;

namespace Baked.Test.Domain;

public class BuildingMetadataModel : TestSpec
{
    [Test]
    public void Builds_metadata_model_for_given_type()
    {
        var parent = GiveMe.TheTypeModel<Parent>();
        var metadataBuilder = new MetadataModelBuilder(new()
        {
            TypeAttributes = [typeof(EntityAttribute), typeof(ControllerModelAttribute)]
        });

        var typeMetadataModel = metadataBuilder.Build(parent.GetMetadata());

        typeMetadataModel.Name.ShouldBe("Parent");
        typeMetadataModel.Attributes.ShouldContain(
            new AttributeMetadataModel(nameof(EntityAttribute))
        );
        typeMetadataModel.Attributes.ShouldContain(
            new AttributeMetadataModel(nameof(ControllerModelAttribute),
                ("Id", "Baked.Playground.Orm.Parent"),
                ("ClassName", "Parent"),
                ("GroupName", "Parents")
            )
        );
        typeMetadataModel.Properties.ShouldContain(p => p.Name == nameof(Parent.Id));
        typeMetadataModel.Properties.ShouldContain(p => p.Name == nameof(Parent.Name));
        typeMetadataModel.Properties.ShouldContain(p => p.Name == nameof(Parent.Surname));
        typeMetadataModel.Properties.ShouldContain(p => p.Name == nameof(Parent.Description));
    }
}