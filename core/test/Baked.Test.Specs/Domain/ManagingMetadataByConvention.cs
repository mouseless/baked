using Baked.Playground.Business;

namespace Baked.Test.Domain;

public class ManagingMetadataByConvention : TestSpec
{
    [Test]
    public void Adding_metadata_to_type()
    {
        var @class = GiveMe.TheTypeModel<Class>().GetMetadata();

        @class.Has<Custom>().ShouldBeTrue();
    }

    [Test]
    public void Modifying_metadata_of_type()
    {
        var @class = GiveMe.TheTypeModel<Class>().GetMetadata();

        @class.Get<Custom>().Value.ShouldBe("FROM CONVENTION");
    }

    [Test]
    public void Adding_metadata_to_property()
    {
        var @class = GiveMe.TheTypeModel<Record>().GetMembers();
        var property = @class.Properties[nameof(Record.Text)];

        property.Has<Custom>().ShouldBeTrue();
    }

    [Test]
    public void Modifying_metadata_of_property()
    {
        var @class = GiveMe.TheTypeModel<Record>().GetMembers();
        var property = @class.Properties[nameof(Record.Text)];

        property.Get<Custom>().Value.ShouldBe("FROM CONVENTION");
    }

    [Test]
    public void Adding_metadata_to_method()
    {
        var @class = GiveMe.TheTypeModel<Class>().GetMembers();
        var method = @class.Methods[nameof(Class.Method)];

        method.Has<Custom>().ShouldBeTrue();
    }

    [Test]
    public void Modifying_metadata_of_method()
    {
        var @class = GiveMe.TheTypeModel<Class>().GetMembers();
        var method = @class.Methods[nameof(Class.Method)];

        method.Get<Custom>().Value.ShouldBe("FROM CONVENTION");
    }

    [Test]
    public void Adding_metadata_to_parameter()
    {
        var @class = GiveMe.TheTypeModel<MethodSamples>().GetMembers();
        var method = @class.GetMethod(nameof(MethodSamples.BuiltInTypeParameters));
        var parameter = method.Parameters["string"];

        parameter.Has<Custom>().ShouldBeTrue();
    }

    [Test]
    public void Modifying_metadata_of_parameter()
    {
        var @class = GiveMe.TheTypeModel<MethodSamples>().GetMembers();
        var method = @class.GetMethod(nameof(MethodSamples.BuiltInTypeParameters));
        var parameter = method.Parameters["string"];

        parameter.Get<Custom>().Value.ShouldBe("FROM CONVENTION");
    }
}