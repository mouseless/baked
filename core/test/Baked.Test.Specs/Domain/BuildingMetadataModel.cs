using Baked.Domain.Metadata;
using Baked.Orm;
using Baked.Playground.Orm;
using Baked.RestApi.Model;
using static Baked.Domain.Metadata.TypeMetadataModel;

namespace Baked.Test.Domain;

public class BuildingMetadataModel : TestSpec
{
    [Test]
    public void Builds_metadata_model_for_given_type()
    {
        var domain = GiveMe.TheDomainModel();
        var metadataBuilder = new MetadataModelBuilder(new());

        var metadatModel = metadataBuilder.Build(domain);

        var typeMetadataModel = metadatModel.Types[typeof(Parent).GetCSharpFriendlyFullName()];
        typeMetadataModel.Name.ShouldBe("Parent");
        typeMetadataModel.Properties.ShouldContain(p => p.Name == nameof(Parent.Id));
        typeMetadataModel.Properties.ShouldContain(p => p.Name == nameof(Parent.Name));
        typeMetadataModel.Properties.ShouldContain(p => p.Name == nameof(Parent.Surname));
        typeMetadataModel.Properties.ShouldContain(p => p.Name == nameof(Parent.Description));
        typeMetadataModel.Attributes.ShouldContain(new AttributeMetadataModel(nameof(EntityAttribute)));
        typeMetadataModel.Attributes.ShouldContain(
            new AttributeMetadataModel(nameof(ControllerModelAttribute),
                ("Id", "Baked.Playground.Orm.Parent"),
                ("ClassName", "Parent"),
                ("GroupName", "Parents")
            )
        );
    }

    [Test]
    public void Type_attributes_can_be_filtered()
    {
        var domain = GiveMe.TheDomainModel();
        var metadataBuilder = new MetadataModelBuilder(new()
        {
            TypeAttributes = [typeof(EntityAttribute)]
        });

        var metadatModel = metadataBuilder.Build(domain);

        var typeMetadataModel = metadatModel.Types[typeof(Parent)];
        typeMetadataModel.Attributes.ShouldContain(a => a.Type == nameof(EntityAttribute));
        typeMetadataModel.Attributes.ShouldNotContain(a => a.Type == nameof(ControllerModelAttribute));
    }

    public class NotExistingAttribute : Attribute;

    [Test]
    public void Type_having_no_matching_attributes_can_be_excluded()
    {
        var domain = GiveMe.TheDomainModel();
        var metadataBuilder = new MetadataModelBuilder(new()
        {
            TypeAttributes = [typeof(NotExistingAttribute)],
            ExcludeTypesMissingAttributes = true
        });

        var metadatModel = metadataBuilder.Build(domain);

        metadatModel.Types.Count.ShouldBe(0);
    }
}