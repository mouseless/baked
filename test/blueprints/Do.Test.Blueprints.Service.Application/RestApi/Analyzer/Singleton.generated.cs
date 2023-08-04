// This file will be auto-generated

using Microsoft.AspNetCore.Mvc;

namespace Do.Test.RestApi.Analyzer;

[ApiController]
public class SingletonController
{
    readonly Singleton _target;

    public SingletonController(Singleton target) =>
      _target = target;

    [HttpGet]
    [Produces("application/json")]
    [Route("singleton/time")]
    public DateTime GetNow()
    {
        var result = _target.GetNow();

        return result;
    }
}
