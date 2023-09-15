// This file will be auto-generated

using Microsoft.AspNetCore.Mvc;

namespace Do.Test;

[ApiController]
public class DemoController
{
    [HttpPost]
    [Produces("application/json")]
    [Route("object-types")]
    public object ObjectPost([FromQuery] object requestQuery) => requestQuery;

    [HttpGet]
    [Produces("application/json")]
    [Route("object-types")]
    public async Task<object> ObjectGet([FromRoute] object request)
    {
        await Task.Delay(10);

        return request;
    }

    [HttpPut]
    [Produces("application/json")]
    [Route("object-types")]
    public async Task<object> ObjectPut([FromBody] object request)
    {
        await Task.Delay(10);

        return request;
    }
}
