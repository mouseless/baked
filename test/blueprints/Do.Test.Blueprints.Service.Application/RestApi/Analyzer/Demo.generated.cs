// This file will be auto-generated

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Do.Test;

[ApiController]
public class DemoController
{
    [HttpPost]
    [Produces("application/json")]
    [Route("object-types")]
    public async Task<object> Object([FromBody] object request)
    {
        await Task.Delay(10);

        return request;
    }
}
