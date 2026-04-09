using Baked.Business;
using Baked.Domain.Metadata;
using Baked.Domain.Model;
using Baked.Orm;
using Baked.Playground.Orm;
using Baked.RestApi.Model;

namespace Baked.Test.Domain;

public class BuildingMetadataModel : TestSpec
{
    public class NotExistingAttribute : Attribute;

    [Test]
    public void Builds_metadata_model_from_domain_model()
    {
        var domain = GiveMe.TheDomainModel();
        var metadataBuilder = new MetadataSetBuilder(new());

        var metadataModel = metadataBuilder.Build(domain);

        var domainMetadataTypes = domain.Types.Where(t => t is TypeModelMetadata);
        metadataModel.Types.All(t => domainMetadataTypes.Any(dt => ((IModel)dt).Id == t.Id)).ShouldBe(true);
    }

    [Test]
    public void Includes_given_attributes_data_for_types()
    {
        var domain = GiveMe.TheDomainModel();
        var metadataBuilder = new MetadataSetBuilder(new()
        {
            TypeAttributes = [typeof(EntityAttribute), typeof(ControllerModelAttribute)]
        });

        var metadataModel = metadataBuilder.Build(domain);

        var typeMetadataModel = metadataModel.Types[typeof(Parent)];
        var attributes = typeMetadataModel.Attributes;
        attributes.Count.ShouldBe(2);
        attributes.ShouldContain(a => a.Type == nameof(EntityAttribute));
        attributes.ShouldContain(a => a.Type == nameof(ControllerModelAttribute) &&
            a.Values.Any(v => v.Key == nameof(ControllerModelAttribute.Id) && $"{v.Value}" == "Baked.Playground.Orm.Parent") &&
            a.Values.Any(v => v.Key == nameof(ControllerModelAttribute.ClassName) && $"{v.Value}" == "Playground_Orm_Parent") &&
            a.Values.Any(v => v.Key == nameof(ControllerModelAttribute.GroupName) && $"{v.Value}" == "Parents")
        );
    }

    [Test]
    public void Types_having_no_matching_attributes_can_be_excluded()
    {
        var domain = GiveMe.TheDomainModel();
        var metadataBuilder = new MetadataSetBuilder(new()
        {
            TypeAttributes = [typeof(NotExistingAttribute)],
            ExcludeTypesMissingAttributes = true
        });

        var metadataModel = metadataBuilder.Build(domain);

        metadataModel.Types.Count.ShouldBe(0);
    }

    [Test]
    public void Metadata_can_include_methods()
    {
        var domain = GiveMe.TheDomainModel();
        var metadataBuilder = new MetadataSetBuilder(new()
        {
            MethodAttributes = [typeof(ActionModelAttribute)],
            ParameterAttributes = [typeof(ParameterModelAttribute)]
        });

        var metadataModel = metadataBuilder.Build(domain);

        var typeMetadataModel = metadataModel.Types[typeof(Parent)];
        var methods = typeMetadataModel.Methods;
        methods.Count.ShouldBe(6);
        methods.ShouldContain(m => m.Name == nameof(Parent.With));
        methods.ShouldContain(m => m.Name == nameof(Parent.GetChildren));
        methods.ShouldContain(m => m.Name == nameof(Parent.AddChild));
        methods.ShouldContain(m => m.Name == nameof(Parent.Update));
        methods.ShouldContain(m => m.Name == nameof(Parent.RemoveChild));
        methods.ShouldContain(m => m.Name == nameof(Parent.Delete));
        var method = typeMetadataModel.Methods.First(m => m.Name == "With");
        method.Parameters.ShouldNotBeNull();
        method.Parameters.Count.ShouldBe(2);
        method.Parameters[0].Attributes.ShouldContain(a => a.Type == nameof(ParameterModelAttribute));
    }

    [Test]
    public void Excludes_methods_not_having_given_attribute()
    {
        var domain = GiveMe.TheDomainModel();
        var metadataBuilder = new MetadataSetBuilder(new()
        {
            MethodAttributes = [typeof(InitializerAttribute)]
        });

        var metadataModel = metadataBuilder.Build(domain);

        var typeMetadataModel = metadataModel.Types[typeof(Parent)];
        var methods = typeMetadataModel.Methods;
        methods.Count.ShouldBe(1);
        methods.ShouldContain(m => m.Name == nameof(Parent.With));
    }

    [Test]
    public void Does_not_include_methods_when_not_configued()
    {
        var domain = GiveMe.TheDomainModel();
        var metadataBuilder = new MetadataSetBuilder(new());

        var metadataModel = metadataBuilder.Build(domain);

        var typeMetadataModel = metadataModel.Types[typeof(Parent)];
        typeMetadataModel.Methods.Count.ShouldBe(0);
    }

    [Test]
    public void Does_not_include_parameters_when_not_configured()
    {
        var domain = GiveMe.TheDomainModel();
        var metadataBuilder = new MetadataSetBuilder(new()
        {
            MethodAttributes = [typeof(InitializerAttribute)]
        });

        var metadataModel = metadataBuilder.Build(domain);

        var typeMetadataModel = metadataModel.Types[typeof(Parent)];
        var method = typeMetadataModel.Methods.First(m => m.Name == nameof(Parent.With));
        method.Parameters.Count.ShouldBe(0);
    }

    [Test]
    public void Metadata_can_includes_properties()
    {
        var domain = GiveMe.TheDomainModel();
        var metadataBuilder = new MetadataSetBuilder(new()
        {
            PropertyAttributes = [typeof(IdAttribute), typeof(LabelAttribute)]
        });

        var metadataModel = metadataBuilder.Build(domain);

        var typeMetadataModel = metadataModel.Types[typeof(Parent)];
        var properties = typeMetadataModel.Properties;
        properties.Count.ShouldBe(3);
        properties.ShouldContain(p => p.Name == nameof(Parent.Id));
        properties.ShouldContain(p => p.Name == nameof(Parent.Name));
        properties.ShouldContain(p => p.Name == nameof(Parent.Surname));
    }

    [Test]
    public void Properties_can_be_excluded()
    {
        var domain = GiveMe.TheDomainModel();
        var metadataBuilder = new MetadataSetBuilder(new()
        {
            PropertyAttributes = [typeof(IdAttribute)]
        });

        var metadataModel = metadataBuilder.Build(domain);

        var typeMetadataModel = metadataModel.Types[typeof(Parent)];
        var properties = typeMetadataModel.Properties;
        properties.Count.ShouldBe(1);
        properties.ShouldContain(p => p.Name == nameof(Parent.Id));
    }

    [Test]
    public void Does_not_include_properties_when_not_configued()
    {
        var domain = GiveMe.TheDomainModel();
        var metadataBuilder = new MetadataSetBuilder(new());

        var metadataModel = metadataBuilder.Build(domain);

        var typeMetadataModel = metadataModel.Types[typeof(Parent)];
        var properties = typeMetadataModel.Properties;
        properties.Count.ShouldBe(0);
    }
}