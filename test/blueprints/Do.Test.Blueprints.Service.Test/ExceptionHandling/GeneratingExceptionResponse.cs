using Do.Architecture;
using Do.Test.RestApi.Analyzer;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;

namespace Do.Test.ExceptionHandling;

public class GeneratingExceptionResponse : TestServiceNfr
{
    protected override Application ForgeApplication() =>
         Forge.New
             .Service(
                 business: c => c.Default(assemblies: [typeof(Entity).Assembly], controllerAssembly: typeof(ParentsController).Assembly),
                 database: c => c.InMemory(),
                 exceptionHandling: ex => ex.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}")
             );

    [Test]
    public async Task Handled_exception_problem_details_are_set_by_its_handler()
    {
        var response = await Client.PostAsync($"singleton/test-exception?handled=true", null);

        var problemDetails = response.Content.ReadFromJsonAsync<ProblemDetails>().Result;

        problemDetails.ShouldNotBeNull();
        problemDetails.Detail.ShouldBe("A handled exception was thrown");
        problemDetails.Status.ShouldBe((int)HttpStatusCode.BadRequest);
        problemDetails.Type.ShouldBe("https://do.mouseless.codes/errors/test-service-handled");
    }

    [Test]
    public async Task Unhandled_exception_problem_details_are_set_by_unhandled_exception_handler()
    {
        var response = await Client.PostAsync($"singleton/test-exception?handled=false", null);

        var problemDetails = response.Content.ReadFromJsonAsync<ProblemDetails>().Result;

        problemDetails.ShouldNotBeNull();
        problemDetails.Detail.ShouldBe("An unexpected error has occured. Please contact the administrator.");
        problemDetails.Status.ShouldBe((int)HttpStatusCode.InternalServerError);
        problemDetails.Type.ShouldBe("https://do.mouseless.codes/errors/invalid-operation");
    }
}
