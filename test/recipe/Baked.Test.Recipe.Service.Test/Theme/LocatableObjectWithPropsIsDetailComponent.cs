using Baked.Test.CodingStyle.RichTransient;
using Baked.Test.Orm;
using Baked.Theme.Admin;

namespace Baked.Test.Theme;

public class LocatableObjectWithPropsIsDetailComponent : TestServiceSpec
{
    [Test]
    public void Locatable_classes_with_public_properties_have_detail_attribute(
        [Values(
            typeof(Child),
            typeof(Entity),
            typeof(Parent),
            typeof(RichTransientWithData)
        )]
        Type type
    )
    {
        var domainModel = GiveMe.TheDomainModel();

        domainModel.Types[type].GetMetadata().Has<DetailAttribute>().ShouldBeTrue();
    }

    [Test]
    public void Detail_public_properties_have_detail_property_attribute()
    {
        var domainModel = GiveMe.TheDomainModel();

        var members = domainModel.Types[typeof(RichTransientWithData)].GetMembers();
        var detailProperties = members.Properties.Where(p => p.Has<DetailPropertyAttribute>());
        detailProperties.ShouldNotBeNull();
        detailProperties.Count().ShouldBe(2);
        detailProperties.ShouldContain(p => p.Name == nameof(RichTransientWithData.Id));
        detailProperties.ShouldContain(p => p.Name == nameof(RichTransientWithData.Time));
    }

    [Test]
    public void Detail_public_properties_have_table_column_attribute()
    {
        var domainModel = GiveMe.TheDomainModel();

        var members = domainModel.Types[typeof(Child)].GetMembers();
        var detailProperties = members.Properties.Where(p => p.Has<TableColumnAttribute>());
        detailProperties.ShouldNotBeNull();
        detailProperties.Count().ShouldBe(2);
        detailProperties.ShouldContain(p => p.Name == nameof(Child.Id));
        detailProperties.ShouldContain(p => p.Name == nameof(Child.Parent));
    }

    [Test]
    public void Detail_public_methods_with_no_parameters_and_returns_list_have_table_attribute()
    {
        var domainModel = GiveMe.TheDomainModel();

        var members = domainModel.Types[typeof(Parent)].GetMembers();
        var tableMethods = members.Methods.Where(p => p.Has<TableAttribute>());
        tableMethods.ShouldNotBeNull();
        tableMethods.Count().ShouldBe(1);
        tableMethods.ShouldContain(p => p.Name == nameof(Parent.GetChildren));
    }
}