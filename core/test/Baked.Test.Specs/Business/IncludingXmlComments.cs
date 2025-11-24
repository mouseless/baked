namespace Baked.Test.Business;

public class IncludingXmlComments : TestSpec
{
    [Test]
    public void Accessing_class_documentation()
    {
        var domainModel = GiveMe.TheDomainModel();
        var members =
            domainModel
                .Types[typeof(DocumentationSamples)]
                .GetMembers();

        var summary = members.Documentation?["summary"];
        summary.ShouldNotBeNull();
        summary.InnerText.Trim().ShouldBe("Class summary");
    }

    [Test]
    public void Accessing_method_documentation()
    {
        var domainModel = GiveMe.TheDomainModel();
        var method =
            domainModel
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

    [Test]
    public void Accessing_method_documentation_of_a_parameterless_method()
    {
        var domainModel = GiveMe.TheDomainModel();
        var method =
            domainModel
                .Types[typeof(DocumentationSamples)]
                .GetMembers()
                .GetMethod(nameof(DocumentationSamples.ParameterlessMethod));

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
        var domainModel = GiveMe.TheDomainModel();
        var parameter =
            domainModel
                .Types[typeof(DocumentationSamples)]
                .GetMembers()
                .GetMethod(nameof(DocumentationSamples.Method))
                .Parameters[name];

        var documentation = parameter.Documentation?.InnerText;
        documentation.ShouldNotBeNull();
        documentation.Trim().ShouldBe(expectedDocumentation);
    }

    [TestCase("enumerable", "Enumerable description")]
    [TestCase("array", "Array description")]
    [TestCase("dictionary", "Dictionary description")]
    public void Accessing_array_and_generic_parameter_documentation(string name, string expectedDocumentation)
    {
        var domainModel = GiveMe.TheDomainModel();
        var parameter =
            domainModel
                .Types[typeof(DocumentationSamples)]
                .GetMembers()
                .GetMethod(nameof(DocumentationSamples.ArrayAndGenericParameters))
                .Parameters[name];

        var documentation = parameter.Documentation?.InnerText;
        documentation.ShouldNotBeNull();
        documentation.Trim().ShouldBe(expectedDocumentation);
    }

    [Test]
    public void Accessing_property_documentation()
    {
        var domainModel = GiveMe.TheDomainModel();
        var property =
            domainModel
                .Types[typeof(DocumentedData)]
                .GetMembers()
                .Properties[nameof(DocumentedData.Property)];

        var summary = property.Documentation?["summary"];
        summary.ShouldNotBeNull();
        summary.InnerText.Trim().ShouldBe("Property summary");
    }

    [Test]
    public void Xml_comments_for_generic_classes_are_not_included_for_now()
    {
        var domainModel = GiveMe.TheDomainModel();
        var type =
            domainModel
                .Types[typeof(DocumentationSamples.Generic<>)]
                .GetMembers();

        var summary = type.Documentation?["summary"];
        summary.ShouldBeNull();
    }
}