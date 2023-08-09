// This file will be auto-generated

using Microsoft.AspNetCore.Mvc;

namespace Do.Test.RestApi.Analyzer;

[ApiController]
public class SingletonController
{
    readonly IServiceProvider _serviceProvider;

    public SingletonController(IServiceProvider serviceProvider) =>
        _serviceProvider = serviceProvider;

    [HttpGet]
    [Produces("application/json")]
    [Route("singleton/time")]
    public DateTime GetNow()
    {
        var target = _serviceProvider.GetRequiredService<Singleton>();

        return target.GetNow();
    }
}
