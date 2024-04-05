using Do.Architecture;
using Do.Authentication;
using Do.ExceptionHandling;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;

namespace Do.Test.ExceptionHandling;

public class GeneratingUnauthorizedAccessResponse : TestServiceNfr
{
    protected override Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>? Authentication =>
        c => c.FixedToken();
    protected override Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? ExceptionHandling =>
       c => c.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}");

    [Test]
    public async Task Unauthorized_access_exceptions_are_handled_with_its_own_handler()
    {
        var response = await Client.PostAsync("authentication-samples/token-authentication", null);

        var problemDetails = response.Content.ReadFromJsonAsync<ProblemDetails>().Result;

        problemDetails.ShouldNotBeNull();
        problemDetails.Detail.ShouldBe("Attempted to perform an unauthorized operation.");
        problemDetails.Status.ShouldBe((int)HttpStatusCode.Unauthorized);
        problemDetails.Title.ShouldBe("Unauthorized Access");
        problemDetails.Type.ShouldBe("https://do.mouseless.codes/errors/unauthorized-access");
    }
}