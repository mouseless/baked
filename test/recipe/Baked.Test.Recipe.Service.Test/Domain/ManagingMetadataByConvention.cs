using Baked.Test.Business;

namespace Baked.Test.Domain;

public class ManagingMetadataByConvention : TestServiceSpec
{
    [Test]
    public void Adding_metadata_to_type()
    {
        var @class = GiveMe.TheTypeModel<Class>().GetMetadata();

        @class.Has<CustomAttribute>().ShouldBeTrue();
    }

    [Test]
    public void Modifying_metadata_of_type()
    {
        var @class = GiveMe.TheTypeModel<Class>().GetMetadata();

        @class.Get<CustomAttribute>().Value.ShouldBe("FROM CONVENTION");
    }

    [Test]
    public void Adding_metadata_to_property()
    {
        var @class = GiveMe.TheTypeModel<Record>().GetMembers();
        var property = @class.Properties[nameof(Record.Text)];

        property.Has<CustomAttribute>().ShouldBeTrue();
    }

    [Test]
    public void Modifying_metadata_of_property()
    {
        var @class = GiveMe.TheTypeModel<Record>().GetMembers();
        var property = @class.Properties[nameof(Record.Text)];

        property.Get<CustomAttribute>().Value.ShouldBe("FROM CONVENTION");
    }

    [Test]
    public void Adding_metadata_to_method()
    {
        var @class = GiveMe.TheTypeModel<Class>().GetMembers();
        var method = @class.Methods[nameof(Class.Method)];

        method.Has<CustomAttribute>().ShouldBeTrue();
    }

    [Test]
    public void Modifying_metadata_of_method()
    {
        var @class = GiveMe.TheTypeModel<Class>().GetMembers();
        var method = @class.Methods[nameof(Class.Method)];

        method.Get<CustomAttribute>().Value.ShouldBe("FROM CONVENTION");
    }

    [Test]
    public void Adding_metadata_to_parameter()
    {
        var @class = GiveMe.TheTypeModel<MethodSamples>().GetMembers();
        var method = @class.GetMethod(nameof(MethodSamples.PrimitiveParameters));
        var parameter = method.Parameters["string"];

        parameter.Has<CustomAttribute>().ShouldBeTrue();
    }

    [Test]
    public void Modifying_metadata_of_parameter()
    {
        var @class = GiveMe.TheTypeModel<MethodSamples>().GetMembers();
        var method = @class.GetMethod(nameof(MethodSamples.PrimitiveParameters));
        var parameter = method.Parameters["string"];

        parameter.Get<CustomAttribute>().Value.ShouldBe("FROM CONVENTION");
    }
}