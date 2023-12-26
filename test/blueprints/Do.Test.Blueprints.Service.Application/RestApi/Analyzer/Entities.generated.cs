// This file will be auto-generated

using Do.Orm;
using Microsoft.AspNetCore.Mvc;

namespace Do.Test;

[ApiController]
public class EntitiesController
{
    public record ByRequest(
        Guid? Guid = default,
        string String = default,
        string StringData = default,
        int? Int32 = default,
        Uri Uri = default,
        Status? Status = default,
        DateTime? DateTime = default
    );

    [HttpGet]
    [Route("entities")]
    public List<Entity> By([FromServices] Entities service, [FromQuery] ByRequest request, [FromQuery] int? take = null, [FromQuery] int? skip = null)
    {
        var result = service.By(
            guid: request.Guid,
            @string: request.String,
            stringData: request.StringData,
            int32: request.Int32,
            uri: request.Uri,
            status: request.Status,
            dateTime: request.DateTime,
            take: take,
            skip: skip
        );

        return result;
    }

    [HttpGet]
    [Route("entities/{id}")]
    public Entity Get([FromServices] IQueryContext<Entity> service, Guid id)
    {
        return service.SingleById(id);
    }

    public record NewRequest(
        Guid Guid = default,
        string String = default,
        string StringData = default,
        int Int32 = default,
        Uri Uri = default,
        object Dynamic = default,
        Status Status = default,
        DateTime DateTime = default
    );

    [HttpPost]
    [Route("entities")]
    public Entity New([FromServices] Func<Entity> newTarget, [FromBody] NewRequest request)
    {
        var target = newTarget();

        return target.With(request.Guid, request.String, request.StringData, request.Int32, request.Uri, request.Dynamic, request.Status, request.DateTime);
    }

    [HttpDelete]
    [Route("entities/{id}")]
    public void Delete([FromServices] IQueryContext<Entity> service, [FromRoute] Guid id)
    {
        var target = service.SingleById(id);

        target.Delete();
    }

    public record UpdateRequest(
        Guid Guid = default,
        string String = default,
        string StringData = default,
        int Int32 = default,
        Uri Uri = default,
        object Dynamic = default,
        Status Status = default,
        DateTime DateTime = default,
        bool useTransaction = false,
        bool throwError = false
    );

    [HttpPut]
    [Route("entities/{id}")]
    public async Task Update([FromServices] IQueryContext<Entity> service, [FromRoute] Guid id, [FromBody] UpdateRequest request)
    {
        var target = service.SingleById(id);

        await target.Update(request.Guid, request.String, request.StringData, request.Int32, request.Uri, request.Dynamic, request.Status, request.DateTime,
            useTransaction: request.useTransaction,
            throwError: request.throwError
        );
    }
}
