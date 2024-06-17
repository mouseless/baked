namespace Baked.Test.Business;

public class IncludingXmlComments : TestServiceSpec
{
    [Test]
    public void Accessing_class_documentation()
    {
        var members =
            DomainModel
                .Types[typeof(DocumentationSamples)]
                .GetMembers();

        var summary = members.Documentation?["summary"];
        summary.ShouldNotBeNull();
        summary.InnerText.Trim().ShouldBe("Class summary");
    }

    [Test]
    public void Accessing_method_documentation()
    {
        var method =
            DomainModel
                .Types[typeof(DocumentationSamples)]
                .GetMembers()
                .GetMethod(nameof(DocumentationSamples.Method));

        var summary = method.Documentation?["summary"];
        summary.ShouldNotBeNull();
        summary.InnerText.Trim().ShouldBe("Method summary");

        var returns = method.Documentation?["returns"];
        returns.ShouldNotBeNull();
        returns.InnerText.Trim().ShouldBe("Return documentation");
    }

    [TestCase("parameter1", "Parameter 1 documentation")]
    [TestCase("parameter2", "Parameter 2 documentation")]
    public void Accessing_parameter_documentation(string name, string expectedDocumentation)
    {
        var parameter =
            DomainModel
                .Types[typeof(DocumentationSamples)]
                .GetMembers()
                .GetMethod(nameof(DocumentationSamples.Method))
                .Parameters[name];

        var documentation = parameter.Documentation?.InnerText;
        documentation.ShouldNotBeNull();
        documentation.Trim().ShouldBe(expectedDocumentation);
    }

    [Test]
    public void Accessing_property_documentation()
    {
        var property =
            DomainModel
                .Types[typeof(DocumentedData)]
                .GetMembers()
                .Properties[nameof(DocumentedData.Property)];

        var summary = property.Documentation?["summary"];
        summary.ShouldNotBeNull();
        summary.InnerText.Trim().ShouldBe("Property summary");

        var value = property.Documentation?["value"];
        value.ShouldNotBeNull();
        value.InnerText.Trim().ShouldBe("Value explanation");
    }
}