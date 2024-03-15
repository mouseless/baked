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
        string Uniq = default,
        Uri Uri = default,
        Status? Status = default,
        DateTime? DateTime = default,
        int? Take = null,
        int? Skip = null
    );

    [HttpGet]
    [Route("entities")]
    public List<Entity> By([FromServices] Entities target, [FromQuery] ByRequest request)
    {
        return target.By(
            guid: request.Guid,
            @string: request.String,
            stringData: request.StringData,
            int32: request.Int32,
            uniq: request.Uniq,
            uri: request.Uri,
            status: request.Status,
            dateTime: request.DateTime,
            take: request.Take,
            skip: request.Skip
        );
    }

    public record SingleByStringRequest(string Uniq);

    [HttpGet]
    [Route("entities/single-by-uniq")]
    public Entity SingleByUniq([FromServices] Entities target, [FromQuery] SingleByStringRequest request)
    {
        return target.SingleByUniq(request.Uniq);
    }

    public record FirstByStringRequest(string StartsWith,
        bool Asc = false,
        bool Desc = false
    );

    [HttpGet]
    [Route("entities/first-by-string")]
    public Entity FirstByString([FromServices] Entities target, [FromQuery] FirstByStringRequest request)
    {
        return target.FirstByString(request.StartsWith,
            asc: request.Asc,
            desc: request.Desc
        );
    }

    [HttpGet]
    [Route("entities/{id}")]
    public Entity Get([FromServices] IQueryContext<Entity> entityQuery, Guid id)
    {
        return entityQuery.SingleById(id);
    }

    public record NewRequest(
        Guid Guid = default,
        string String = default,
        string StringData = default,
        int Int32 = default,
        string Uniq = default,
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

        return target.With( request.Uniq, request.Guid, request.String, request.StringData, request.Int32, request.Uri, request.Dynamic, request.Status, request.DateTime);
    }

    [HttpDelete]
    [Route("entities/{id}")]
    public void Delete([FromServices] IQueryContext<Entity> entityQuery, [FromRoute] Guid id)
    {
        var target = entityQuery.SingleById(id);

        target.Delete();
    }

    public record UpdateRequest(
        Guid Guid = default,
        string String = default,
        string StringData = default,
        int Int32 = default,
        string Uniq = default,
        Uri Uri = default,
        object Dynamic = default,
        Status Status = default,
        DateTime DateTime = default,
        bool useTransaction = false,
        bool throwError = false
    );

    [HttpPut]
    [Route("entities/{id}")]
    public async Task Update([FromServices] IQueryContext<Entity> entityQuery, [FromRoute] Guid id, [FromBody] UpdateRequest request)
    {
        var target = entityQuery.SingleById(id);

        await target.Update(request.Uniq, request.Guid, request.String, request.StringData, request.Int32, request.Uri, request.Dynamic, request.Status, request.DateTime,
            useTransaction: request.useTransaction,
            throwError: request.throwError
        );
    }
}
