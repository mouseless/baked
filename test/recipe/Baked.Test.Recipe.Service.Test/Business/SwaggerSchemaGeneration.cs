using Newtonsoft.Json.Linq;

namespace Baked.Test.Business;

public class SwaggerSchemaGeneration : TestServiceNfr
{
    [Test]
    public async Task Generates_swagger_json_automatically()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");
        dynamic? content = await response.Content.Deserialize();

        ((string?)content?.paths["/time-provider-samples/now"].get.tags[0]).ShouldBe("TimeProviderSamples");
    }

    [Test]
    public async Task Class_summary_section_is_placed_in_tags_description()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");
        dynamic? content = await response.Content.Deserialize();
        JArray? tags = content?.tags;
        var tag = tags?.Single(tag => tag["name"]?.Value<string>() == "DocumentationSamples");

        tag.ShouldNotBeNull();
        tag["description"].ShouldBe("Class summary");
    }

    [Test]
    public async Task Method_summary_and_remarks_are_placed_in_endpoint_summary_and_description()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");
        dynamic? content = await response.Content.Deserialize();
        var endpoint = content?.paths["/documentation-samples/method"].post;

        ((string?)endpoint?.summary).ShouldBe("Method summary");
        ((string?)endpoint?.description).ShouldBe("Method description");
    }

    [Test]
    public async Task Escapes_newlines_when_xml_comment_is_multiline()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");
        dynamic? content = await response.Content.Deserialize();
        var endpoint = content?.paths["/documentation-samples/multiline"].post;

        ((string?)endpoint?.summary)?.Trim().ShouldBe("""
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras porta,
        augue ut egestas finibus, purus sem scelerisque nunc, ac hendrerit
        sapien ligula eget tellus.
        """);
        ((string?)endpoint?.description)?.Trim().ShouldBe("""
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras porta,
        augue ut egestas finibus, purus sem scelerisque nunc, ac hendrerit
        sapien ligula eget tellus.

        Aenean sollicitudin elementum neque, at vehicula lacus pretium ac.
        Vivamus ac augue eget leo vehicula mollis. Sed vulputate molestie
        commodo.
        """);
    }

    [Test]
    public async Task Class_summary_and_remarks_are_used_for_non_documented_methods_in_command_classes()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");
        dynamic? content = await response.Content.Deserialize();
        var endpoint = content?.paths["/command"].post;

        ((string?)endpoint?.summary).ShouldBe("Command summary from class");
        ((string?)endpoint?.description).ShouldBe("Command remarks from class");
    }

    [Test]
    public async Task Method_parameter_comments_are_placed_in_schema_property_descriptions()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");

        dynamic? content = await response.Content.Deserialize();
        var schema = content?.components.schemas["test--business--documentation-samples--method-request"];

        ((string?)schema?.properties["parameter1"].description).ShouldBe("Parameter 1 documentation");
        ((string?)schema?.properties["parameter2"].description).ShouldBe("Parameter 2 documentation");
    }

    [TestCase("entity-parameters", "entityId", "Entity description")]
    [TestCase("entity-list-parameters", "entityIds", "Entities description")]
    [TestCase("entity-list-parameters", "otherEntityIds", "Other entities description")]
    public async Task Entity_parameter_comments_are_kept_even_if_their_name_change(string method, string parameter, string expected)
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");

        dynamic? content = await response.Content.Deserialize();
        var schema = content?.components.schemas[$"test--business--method-samples--{method}-request"];

        ((string?)schema?.properties[parameter].description).ShouldBe(expected);
    }

    [Test]
    public async Task Route_parameters_are_placed_in_schema_description_under_parameters()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");

        dynamic? content = await response.Content.Deserialize();
        var endpoint = content?.paths["/documentation-samples/route/{route}"].post;

        ((string?)endpoint?.parameters[0].schema?.description).ShouldBe("route description");
    }

    [Test]
    public async Task Query_parameters_are_placed_in_schema_description_under_parameters()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");

        dynamic? content = await response.Content.Deserialize();
        var endpoint = content?.paths["/redirect-samples/form-post"].post;

        ((string?)endpoint?.parameters[0].schema?.description).ShouldBe("uri description");
    }

    [Test]
    public async Task Form_post_parameter_comments_are_placed_in_schema_property_descriptions()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");

        dynamic? content = await response.Content.Deserialize();
        var endpoint = content?.paths["/redirect-samples/form-post"].post;
        var schema = endpoint?.requestBody.content["multipart/form-data"].schema;

        ((string?)schema?.properties["key"].description).ShouldBe("key description");
    }

    [Test]
    public async Task Documented_properties_are_included_in_schema_property_descriptions()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");

        dynamic? content = await response.Content.Deserialize();
        var schema = content?.components.schemas["test--business--documented-data"];

        ((string?)schema?.description).ShouldBe("Data summary");
        ((string?)schema?.properties["property"].description).ShouldBe("Property summary");
    }

    [Test]
    public async Task Request_examples_are_included()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");
        dynamic? content = await response.Content.Deserialize();
        var endpoint = content?.paths["/documentation-samples/method"].post;

        ((object?)endpoint?.requestBody.content["application/json"].example)
            .ShouldDeeplyBe(new
            {
                parameter1 = "value 1",
                parameter2 = "value 2"
            });
    }

    [Test]
    public async Task Response_examples_are_included()
    {
        var response = await Client.GetAsync("/swagger/samples/swagger.json");
        dynamic? content = await response.Content.Deserialize();
        var endpoint = content?.paths["/documentation-samples/method"].post;

        ((object?)endpoint?.responses["200"].content["application/json"].example)
            .ShouldDeeplyBe(new
            {
                property = "value 1 - value 2"
            });
    }
}