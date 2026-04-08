using Baked.Playground.Business;

namespace Baked.Test.Domain;

public class AutoPropertyResolution : TestSpec
{
    [Test]
    public void PropertyInfo_extension()
    {
        var auto = typeof(Data).GetProperty(nameof(Data.Text));
        var calculated = typeof(Data).GetProperty(nameof(Data.CalculatedText));

        auto?.IsAutoProperty.ShouldBeTrue();
        calculated?.IsAutoProperty.ShouldBeFalse();
    }

    [Test]
    public void PropertyModel_property()
    {
        var data = GiveMe.TheTypeModel<Data>().GetMembers();
        var auto = data.Properties[nameof(Data.Text)];
        var calculated = data.Properties[nameof(Data.CalculatedText)];

        auto.IsAutoProperty.ShouldBeTrue();
        calculated.IsAutoProperty.ShouldBeFalse();
    }
}