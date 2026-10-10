using Baked.Business;
using Baked.Domain.Export;
using Baked.Lifetime;
using Baked.Playground.Business;
using Baked.Playground.CodingStyle.LocateViaId;
using Baked.Playground.Orm;
using Baked.RestApi.Model;
using Baked.Theme;
using Baked.Ui;

using Entity = Baked.Orm.Entity;

namespace Baked.Test.Domain;

public class BuildingAttributeExportSets : TestSpec
{
    public class NotExisting : Attribute;

    AttributeProperties _builders = default!;

    public override void SetUp()
    {
        base.SetUp();

        _builders = new();
        _builders.Set<Locatable>(locatable =>
        [
            new(locatable.QueryType),
            new(locatable.IsAsync)
        ]);
    }

    public override void TearDown()
    {
        base.TearDown();

        _builders = default!;
    }

    [Test]
    public void Attributes_are_included_based_on_usage()
    {
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Entity>();
        attributeExport.Include<ComponentGenerator<Text>>();

        attributeExport.Name.ShouldBe("Test");
        attributeExport.Type.ShouldContain<Entity>();
        attributeExport.Type.ShouldContain<ComponentGenerator<Text>>();
        attributeExport.Method.ShouldNotContain<Entity>();
        attributeExport.Method.ShouldContain<ComponentGenerator<Text>>();
        attributeExport.Parameter.ShouldNotContain<Entity>();
        attributeExport.Parameter.ShouldContain<ComponentGenerator<Text>>();
        attributeExport.Property.ShouldNotContain<Entity>();
        attributeExport.Property.ShouldContain<ComponentGenerator<Text>>();
    }

    [Test]
    public void Does_not_add_attribute_more_then_once()
    {
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Entity>();
        attributeExport.Include<Entity>();

        attributeExport.Type.Count.ShouldBe(1);
    }

    [Test]
    public void Attribute_can_be_removed()
    {
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Entity>();
        attributeExport.Exclude<Entity>();

        attributeExport.Type.Count.ShouldBe(0);
    }

    [Test]
    public void Adds_attribute_to_all_filters_when_usage_is_null()
    {
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Custom>();

        attributeExport.Type.ShouldContain<Custom>();
        attributeExport.Method.ShouldContain<Custom>();
        attributeExport.Parameter.ShouldContain<Custom>();
        attributeExport.Property.ShouldContain<Custom>();
    }

    [Test]
    public void Builds_empty_set_when_no_attributes_are_included()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        model.Types.Count.ShouldBe(0);
    }

    [Test]
    public void Includes_given_attributes_data_for_types()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Locatable>();
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        var typeExport = model.Types[typeof(Parent)];
        var attributes = typeExport.Attributes;
        attributes.Count.ShouldBe(1);
        attributes.ShouldContain(a =>
            a.Type == nameof(Locatable) &&
            $"{a.Values[nameof(Locatable.QueryType)]}".Equals("Baked.Playground.Orm.Parents") &&
            $"{a.Values[nameof(Locatable.IsAsync)]}".Equals("false", StringComparison.InvariantCultureIgnoreCase)
        );
    }

    [Test]
    public void Attribute_is_exported_with_its_export_name_when_it_provides_one()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Entity>();
        attributeExport.Include<IdProperty>();
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        var idProperty = model.Types[typeof(Parent)].Properties.First(p => p.Name == nameof(Parent.Id));
        idProperty.Attributes.ShouldContain(a => a.Type == "Id");
        idProperty.Attributes.ShouldNotContain(a => a.Type == nameof(IdProperty));
    }

    [Test]
    public void Included_attribute_can_be_filtered()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Locatable>()
            .AddFilter((locatable, _) => locatable.IsAsync);
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        model.Types.Any(t => t.Name is nameof(Parent));
    }

    [Test]
    public void Type_filters_applies_to_class_interface_struct_and_enum()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Namespace>();
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        var classExport = model.Types[typeof(Parent)];
        classExport.Attributes.ShouldContain(a => a.Type == nameof(Namespace));
        var interfaceExport = model.Types[typeof(ILocatable)];
        interfaceExport.Attributes.ShouldContain(a => a.Type == nameof(Namespace));
        var structExport = model.Types[typeof(Struct)];
        structExport.Attributes.ShouldContain(a => a.Type == nameof(Namespace));
        var enumExport = model.Types[typeof(Enum)];
        enumExport.Attributes.ShouldContain(a => a.Type == nameof(Namespace));
    }

    [Test]
    public void Attribute_values_can_be_removed()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Locatable>()
            .ExcludeProperty(p => p.Name is nameof(Locatable.IsAsync));
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        var typeExport = model.Types[typeof(Parent)];
        var attribute = typeExport.Attributes.First();
        attribute.Values.Single().Key.ShouldBe(nameof(Locatable.QueryType));
    }

    [Test]
    public void Adding_multiple_filters_requires_all_to_return_true()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Locatable>()
            .ExcludeProperty(p => false);
        attributeExport.Include<Locatable>()
            .ExcludeProperty(p => p.Name != nameof(Locatable.QueryType));
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        var typeExport = model.Types[typeof(Parent)];
        var attribute = typeExport.Attributes.First();
        attribute.Values.Single().Key.ShouldBe(nameof(Locatable.QueryType));
    }

    [Test]
    public void Group_name_can_be_configured()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Singleton>();
        attributeExport.TypeGroupName(_ => "GroupName");
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        model.Types.All(t => t.GroupName == "GroupName").ShouldBe(true);
    }

    [Test]
    public void Types_having_no_matching_attributes_are_excluded()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<NotExisting>();
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        model.Types.Count.ShouldBe(0);
    }

    [Test]
    public void Metadata_can_include_methods()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<ApiController>();
        attributeExport.Include<ApiAction>();
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        var typeExport = model.Types[typeof(Parent)];
        var methods = typeExport.Methods;
        methods.Count.ShouldBe(6);
        methods.ShouldContain(m => m.Name == nameof(Parent.With));
        methods.ShouldContain(m => m.Name == nameof(Parent.GetChildren));
        methods.ShouldContain(m => m.Name == nameof(Parent.AddChild));
        methods.ShouldContain(m => m.Name == nameof(Parent.Update));
        methods.ShouldContain(m => m.Name == nameof(Parent.RemoveChild));
        methods.ShouldContain(m => m.Name == nameof(Parent.Delete));
    }

    [Test]
    public void Methods_can_have_parameters()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<ApiController>();
        attributeExport.Include<ApiAction>();
        attributeExport.Include<ApiParameter>();
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        var typeExport = model.Types[typeof(Parent)];
        var method = typeExport.Methods.First(m => m.Name == "With");
        method.Parameters.ShouldNotBeNull();
        method.Parameters.Count.ShouldBe(4);
        method.Parameters[0].Attributes.ShouldContain(a => a.Type == nameof(ApiParameter));
    }

    [Test]
    public void Excludes_methods_not_having_given_attribute()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<ApiController>();
        attributeExport.Include<Initializer>();
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        var typeExport = model.Types[typeof(Parent)];
        var methods = typeExport.Methods;
        methods.Count.ShouldBe(1);
        methods.ShouldContain(m => m.Name == nameof(Parent.With));
    }

    [Test]
    public void Does_not_include_methods_when_not_configued()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<ApiController>();
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        var typeExport = model.Types[typeof(Parent)];
        typeExport.Methods.Count.ShouldBe(0);
    }

    [Test]
    public void Does_not_include_parameters_when_not_configured()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Transient>();
        attributeExport.Include<Initializer>();
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        var typeExport = model.Types[typeof(Parent)];
        var method = typeExport.Methods.First(m => m.Name is nameof(Parent.With));
        method.Parameters.Count.ShouldBe(0);
    }

    [Test]
    public void Metadata_can_includes_properties()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Entity>();
        attributeExport.Include<IdProperty>();
        attributeExport.Include<Label>();

        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        var typeExport = model.Types[typeof(Parent)];
        var properties = typeExport.Properties;
        properties.Count.ShouldBe(3);
        properties.ShouldContain(p => p.Name == nameof(Parent.Id));
        properties.ShouldContain(p => p.Name == nameof(Parent.Name));
        properties.ShouldContain(p => p.Name == nameof(Parent.Surname));
    }

    [Test]
    public void Properties_can_be_excluded()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Entity>();
        attributeExport.Include<IdProperty>();
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        var typeExport = model.Types[typeof(Parent)];
        var properties = typeExport.Properties;
        properties.Count.ShouldBe(1);
        properties.ShouldContain(p => p.Name == nameof(Parent.Id));
    }

    [Test]
    public void Does_not_include_properties_when_not_configued()
    {
        var domain = GiveMe.TheDomainModel();
        var attributeExport = new ExportConfiguration("Test");
        attributeExport.Include<Entity>();
        var builder = new ExportSetBuilder(attributeExport, _builders);

        var model = builder.Build(domain);

        var typeExport = model.Types[typeof(Parent)];
        var properties = typeExport.Properties;
        properties.Count.ShouldBe(0);
    }

}