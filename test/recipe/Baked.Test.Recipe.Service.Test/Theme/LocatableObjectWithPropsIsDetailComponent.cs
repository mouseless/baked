using Baked.Test.CodingStyle.RichTransient;
using Baked.Theme.Admin;
using Baked.Ui;

namespace Baked.Test.Theme;

public class LocatableObjectWithPropsIsDetailComponent : TestServiceSpec
{
    [TestCase(typeof(RichTransientWithData), "/rich-transient-with-datas/{0}")]
    public void Locatable_classes_with_public_properties_have_detail_attribute(Type type, string expectedPath)
    {
        var metadata = GiveMe.TheTypeModel(type).GetMetadata();

        metadata.TryGetSingle<ComponentDescriptorAttribute<Detail>>(out var detail).ShouldBeTrue();
        var data = detail.Data.ShouldBeOfType<RemoteData>();
        data.Path.ShouldBe(expectedPath);
    }

    [Test]
    public void Detail_public_properties_have_detail_property_attribute()
    {
        var domainModel = GiveMe.TheDomainModel();

        var detail = domainModel.Types[typeof(RichTransientWithData)].GetMetadata().GetSingle<ComponentDescriptorAttribute<Detail>>();
        detail.Schema.Props.Count().ShouldBe(2);
        detail.Schema.Props.ShouldContain(p => p.Key == "id");
        detail.Schema.Props.ShouldContain(p => p.Key == "time");
    }
}