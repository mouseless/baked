using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.ExceptionHandling;

public class GeneratingUnauthorizedAccessResponse : TestServiceNfr
{
    [Test]
    public async Task Authentication_exceptions_are_handled_with_its_own_handler()
    {
        Client.DefaultRequestHeaders.Clear();

        var response = await Client.PostAsync("authorization-samples/base-claims", null);

        var problemDetails = response.Content.ReadFromJsonAsync<ProblemDetails>().Result;

        problemDetails.ShouldNotBeNull();
        problemDetails.Detail.ShouldBe("Failed to authenticate with given credentials");
        problemDetails.Status.ShouldBe((int)HttpStatusCode.Unauthorized);
        problemDetails.Title.ShouldBe("Authentication");
        problemDetails.Type.ShouldBe("https://baked.mouseless.codes/errors/authentication");
    }

    [Test]
    public async Task Unauthorized_exceptions_are_handled_with_its_own_handler()
    {
        Client.DefaultRequestHeaders.Authorization = UserFixedBearerToken;

        var response = await Client.PostAsync("authorization-samples/admin", null);

        var problemDetails = response.Content.ReadFromJsonAsync<ProblemDetails>().Result;

        problemDetails.ShouldNotBeNull();
        problemDetails.Detail.ShouldBe("Attempted to perform an unauthorized operation");
        problemDetails.Status.ShouldBe((int)HttpStatusCode.Forbidden);
        problemDetails.Title.ShouldBe("Unauthorized Access");
        problemDetails.Type.ShouldBe("https://baked.mouseless.codes/errors/unauthorized-access");
    }
}