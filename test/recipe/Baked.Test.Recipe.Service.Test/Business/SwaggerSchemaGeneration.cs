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
}