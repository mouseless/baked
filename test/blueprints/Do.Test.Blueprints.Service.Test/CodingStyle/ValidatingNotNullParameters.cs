using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Do.Test.CodingStyle;

public class ValidatingNotNullParameters : TestServiceNfr
{
    [TestCase("value-type", "notNull")]
    [TestCase("reference-type", "notNull")]
    // [TestCase("enum", "notNull")]
    [TestCase("entity", "notNullId")]
    [TestCase("entity-extension", "notNullId")]
    public async Task Not_null_in_query_throws_bad_request_when_not_given(string route, string propertyName)
    {
        var response = await Client.GetAsync($"/nullable-samples/{route}");

        dynamic? result = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        ((object?)result?.errors?[propertyName]).ShouldNotBeNull();
    }

    [TestCase("value-type", "notNull")]
    [TestCase("reference-type", "notNull")]
    // [TestCase("enum", "notNull")]
    [TestCase("entity", "notNullId")]
    [TestCase("entity-extension", "notNullId")]
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
    [TestCase("reference-type", "notNull")]
    // [TestCase("enum", "notNull")]
    [TestCase("entity", "notNullId")]
    [TestCase("entity-extension", "notNullId")]
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