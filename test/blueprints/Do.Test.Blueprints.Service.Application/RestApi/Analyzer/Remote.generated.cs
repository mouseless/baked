// This file will be auto-generated

using Do.Database;
using Microsoft.AspNetCore.Mvc;

namespace Do.Test;

[ApiController]
public class RemoteController
{
    public record ProcessRequest(
        string path,
        string Method,
        object content = default
    );

    [HttpPost]
    [Produces("application/json")]
    [Route("remote")]
    [NoTransaction]
    public async Task<object> Process([FromServices] Remote target, [FromBody] ProcessRequest request)
    {
        return await target.Process(
            request.path,
            request.Method,
            request.content
        );
    }
}
