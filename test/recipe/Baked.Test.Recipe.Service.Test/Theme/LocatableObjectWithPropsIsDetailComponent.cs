using Baked.Test.CodingStyle.RichTransient;
using Baked.Test.Orm;

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
}