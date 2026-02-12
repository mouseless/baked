using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class ValidatingNotNullParameters : TestNfr
{
    [TestCase("value-type", "notNull")]
    [TestCase("enum", "notNull")]
    [TestCase("reference-type", "notNull")]
    [TestCase("entity", "notNullId")]
    [TestCase("entity-extension", "notNullId")]
    [TestCase("rich-transient", "notNullId")]
    public async Task Not_null_in_query_throws_bad_request_when_not_given(string route, string propertyName)
    {
        var response = await Client.GetAsync($"/nullable-samples/{route}");

        dynamic? result = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        ((object?)result?.errors?[propertyName]).ShouldNotBeNull();
    }

    [TestCase("value-type", "notNull")]
    [TestCase("enum", "notNull")]
    [TestCase("reference-type", "notNull")]
    [TestCase("record", "notNull")]
    [TestCase("entity", "notNull")]
    [TestCase("entity-extension", "notNull")]
    [TestCase("rich-transient", "notNull")]
    public async Task Not_null_in_body_throws_bad_request_when_not_given(string route, string propertyName)
    {
        var response = await Client.PostAsync($"/nullable-samples/{route}", JsonContent.Create(
            new { }
        ));

        dynamic? result = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        ((object?)result?.errors?[propertyName]).ShouldNotBeNull();
    }

    [TestCase("value-type", "notNull")]
    [TestCase("enum", "notNull")]
    [TestCase("reference-type", "notNull")]
    [TestCase("record", "notNull")]
    [TestCase("entity", "notNull")]
    [TestCase("entity-extension", "notNull")]
    [TestCase("rich-transient", "notNull")]
    public async Task Not_null_in_body_throws_bad_request_when_null_is_given(string route, string propertyName)
    {
        var response = await Client.PostAsync($"/nullable-samples/{route}", JsonContent.Create(JsonConvert.DeserializeObject($$"""
            { "{{propertyName}}": null }
        """)));

        dynamic? result = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        ((object?)result?.errors?[propertyName]).ShouldNotBeNull();
    }
}