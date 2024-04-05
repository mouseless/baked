using Do.Architecture;
using Do.ExceptionHandling;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;

namespace Do.Test.ExceptionHandling;

public class GeneratingExceptionResponse : TestServiceNfr
{
    protected override Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? ExceptionHandling =>
        c => c.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}");

    [Test]
    public async Task Handled_exception_problem_details_are_set_by_its_handler()
    {
        var response = await Client.PostAsync($"exception-samples/throw?handled=true", null);

        var problemDetails = response.Content.ReadFromJsonAsync<ProblemDetails>().Result;

        problemDetails.ShouldNotBeNull();
        problemDetails.Detail.ShouldBe("A handled exception was thrown");
        problemDetails.Status.ShouldBe((int)HttpStatusCode.BadRequest);
        problemDetails.Type.ShouldBe("https://do.mouseless.codes/errors/test-service-handled");
    }

    [Test]
    public async Task Unhandled_exception_problem_details_are_set_by_unhandled_exception_handler()
    {
        var response = await Client.PostAsync($"exception-samples/throw?handled=false", null);

        var problemDetails = response.Content.ReadFromJsonAsync<ProblemDetails>().Result;

        problemDetails.ShouldNotBeNull();
        problemDetails.Detail.ShouldBe("An unexpected error has occured. Please contact the administrator.");
        problemDetails.Status.ShouldBe((int)HttpStatusCode.InternalServerError);
        problemDetails.Type.ShouldBe("https://do.mouseless.codes/errors/invalid-operation");
    }
}
