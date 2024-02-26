using Do.Architecture;
using Do.Authentication;
using Do.ExceptionHandling;
using System.Net.Http.Headers;

namespace Do.Test.ExceptionHandling;

public class HandlingUnauthorizedAccessExceptions : TestServiceNfr
{
    protected override Func<AuthenticationConfigurator, IFeature<AuthenticationConfigurator>>? Authentication =>
        c => c.FixedToken(["Backend"]);
    protected override Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? ExceptionHandling =>
       c => c.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}");

    [Test]
    public async Task Unauthorized_access_exceptions_are_handled()
    {
        Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Wrong_Token");

        var response = await Client.GetAsync("singleton/time");
        ((int)response.StatusCode).ShouldBe(401);
    }
}
