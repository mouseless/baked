using Baked.Test.Business;

namespace Baked.Test.Testing;

public class AccessingXmlComments : TestSpec
{
    [Test]
    public void Xml_comments_are_available_through_stubbers()
    {
        var documentation = GiveMe.TheDocumentation<DocumentationSamples>();

        documentation.GetSummary().ShouldBe("Class summary");
    }

    [Test]
    public void Method_comments_are_accessed_through_method_name()
    {
        var documentation = GiveMe.TheDocumentation<DocumentationSamples>(method: nameof(DocumentationSamples.Method));

        documentation.GetSummary().ShouldBe("Method summary");
        documentation.GetRemarks().ShouldBe("Method description");
        documentation.GetExampleCode(@for: "request").ShouldBe("""
        {
          "parameter1": "value 1",
          "parameter2": "value 2"
        }
        """);
        documentation.GetExampleCode(@for: "response").ShouldBe("""
        {
          "property": "value 1 - value 2"
        }
        """);
    }

    [Test]
    public void Parameter_comments_are_accessed_through_method_name_and_parameter()
    {
        var documentation = GiveMe.TheDocumentation<DocumentationSamples>(method: nameof(DocumentationSamples.Method), parameter: "parameter1");

        documentation?.InnerText.Trim().ShouldBe("Parameter 1 documentation");
    }

    [Test]
    public void Property_comments_are_accessed_through_property_name()
    {
        var documentation = GiveMe.TheDocumentation<DocumentedData>(property: nameof(DocumentedData.Property));

        documentation.GetSummary().ShouldBe("Property summary");
    }
}