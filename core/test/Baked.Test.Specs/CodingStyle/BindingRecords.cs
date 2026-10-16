using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.CodingStyle;

public class BindingRecords : TestNfr
{
    [Test]
    public async Task RecordParameters()
    {
        var response = await Client.PostAsync("/method-samples/record-parameters", JsonContent.Create(
            new
            {
                record = new { text = "test", numeric = 42 }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task RecordListParameters()
    {
        var response = await Client.PostAsync("/method-samples/record-list-parameters", JsonContent.Create(
            new
            {
                records = new[]
                {
                    new { text = "item 1", numeric = 42 },
                    new { text = "item 2", numeric = 24 }
                }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [TestCase("too long", 5, "Text")]
    [TestCase("text", 42, "Numeric")]
    public async Task Annotated_record_properties_are_validated(string text, int numeric, string invalidProperty)
    {
        var response = await Client.PostAsync("/method-samples/annotated-record-parameters", JsonContent.Create(
            new
            {
                record = new { text, numeric }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Errors.Keys.ShouldContain(key => key.EndsWith(invalidProperty, StringComparison.OrdinalIgnoreCase));
    }

    [Test]
    public async Task Valid_annotated_records_pass_validation()
    {
        var response = await Client.PostAsync("/method-samples/annotated-record-parameters", JsonContent.Create(
            new
            {
                record = new { text = "text", numeric = 5 }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Test]
    public async Task Annotated_record_properties_are_validated_along_with_validatable_records()
    {
        var response = await Client.PostAsync("/method-samples/annotated-record-with-validatable", JsonContent.Create(
            new
            {
                record = new { text = "text", numeric = 42 },
                validatable = new
                {
                    left = new { id = "same" },
                    right = new { id = "same" }
                }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Errors.Keys.ShouldContain(key => key.EndsWith("Numeric", StringComparison.OrdinalIgnoreCase));
    }

    [Test]
    public async Task ValidatingRecords()
    {
        var response = await Client.PostAsync("/method-samples/validatable", JsonContent.Create(
            new
            {
                validatable = new
                {
                    left = new { id = "left" },
                    right = new { id = "right" }
                }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task Validation_results_of_records_are_returned_as_validation_problem_details()
    {
        var response = await Client.PostAsync("/method-samples/validatable-with-results", JsonContent.Create(
            new
            {
                validatable = new
                {
                    left = new { id = "left" },
                    right = new { id = "right" }
                }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Errors.Values.SelectMany(messages => messages).ShouldContain("Left and right should be the same");
    }

    [Test]
    public async Task Valid_records_pass_validation()
    {
        var response = await Client.PostAsync("/method-samples/validatable-with-results", JsonContent.Create(
            new
            {
                validatable = new
                {
                    left = new { id = "same" },
                    right = new { id = "same" }
                }
            }
        ));

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}