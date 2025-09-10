using Baked.Test.Business;

namespace Baked.Test.Domain;

public class ManagingMetadataByConvention : TestServiceSpec
{
    [Test]
    public void Adding_metadata_to_type()
    {
        var model = GiveMe.TheDomainModel();

        var @class = model.Types[typeof(Class)].GetMetadata();

        @class.Has<CustomAttribute>().ShouldBeTrue();
    }

    [Test]
    public void Modifying_metadata_of_type()
    {
        var model = GiveMe.TheDomainModel();

        var @class = model.Types[typeof(Class)].GetMetadata();

        @class.Get<CustomAttribute>().Value.ShouldBe("FROM CONVENTION");
    }

    [Test]
    public void Adding_metadata_to_property()
    {
        var model = GiveMe.TheDomainModel();
        var @class = model.Types[typeof(Record)].GetMembers();

        var property = @class.Properties[nameof(Record.Text)];

        property.Has<CustomAttribute>().ShouldBeTrue();
    }

    [Test]
    public void Modifying_metadata_of_property()
    {
        var model = GiveMe.TheDomainModel();
        var @class = model.Types[typeof(Record)].GetMembers();

        var property = @class.Properties[nameof(Record.Text)];

        property.Get<CustomAttribute>().Value.ShouldBe("FROM CONVENTION");
    }

    [Test]
    public void Adding_metadata_to_method()
    {
        var model = GiveMe.TheDomainModel();
        var @class = model.Types[typeof(Class)].GetMembers();

        var method = @class.Methods[nameof(Class.Method)];

        method.Has<CustomAttribute>().ShouldBeTrue();
    }

    [Test]
    public void Modifying_metadata_of_method()
    {
        var model = GiveMe.TheDomainModel();
        var @class = model.Types[typeof(Class)].GetMembers();

        var method = @class.Methods[nameof(Class.Method)];

        method.Get<CustomAttribute>().Value.ShouldBe("FROM CONVENTION");
    }

    [Test]
    public void Adding_metadata_to_parameter()
    {
        var model = GiveMe.TheDomainModel();
        var @class = model.Types[typeof(MethodSamples)].GetMembers();
        var method = @class.GetMethod(nameof(MethodSamples.PrimitiveParameters));

        var parameter = method.Parameters["string"];

        parameter.Has<CustomAttribute>().ShouldBeTrue();
    }

    [Test]
    public void Modifying_metadata_of_parameter()
    {
        var model = GiveMe.TheDomainModel();
        var @class = model.Types[typeof(MethodSamples)].GetMembers();
        var method = @class.GetMethod(nameof(MethodSamples.PrimitiveParameters));

        var parameter = method.Parameters["string"];

        parameter.Get<CustomAttribute>().Value.ShouldBe("FROM CONVENTION");
    }
}