// This file will be auto-generated

using Do.Orm;
using Microsoft.AspNetCore.Mvc;

namespace Do.Test.RestApi.Analyzer;

[ApiController]
public class EntityController
{
    readonly IQueryContext<Entity> _query;
    readonly Func<Entity> _newTarget;
    readonly Entities _targets;

    public EntityController(IQueryContext<Entity> query, Func<Entity> newTarget, Entities targets) =>
      (_query, _newTarget, _targets) = (query, newTarget, targets);

    [HttpGet]
    [Route("entities")]
    public List<Entity> By([FromQuery] string @string = default)
    {
        return _targets.By(@string);
    }

    [HttpGet]
    [Route("entities/{id}")]
    public Entity Get(Guid id)
    {
        return _query.SingleById(id);
    }

    public record NewRequest(string String, int Int32, Uri Uri, object Dynamic);

    [HttpPost]
    [Route("entities")]

    public Entity New([FromBody] NewRequest request)
    {
        var target = _newTarget();

        return target.With(request.String, request.Int32, request.Uri, request.Dynamic);
    }

    [HttpDelete]
    [Route("entities/{id}")]
    public void Delete([FromRoute] Guid id)
    {
        var target = _query.SingleById(id);

        target.Delete();
    }

    public record UpdateRequest(string String, int Int32, Uri Uri, object Dynamic);

    [HttpPut]
    [Route("entities/{id}")]
    public void Update([FromRoute] Guid id, [FromBody] UpdateRequest request)
    {
        var target = _query.SingleById(id);

        target.Update(request.String, request.Int32, request.Uri, request.Dynamic);
    }
}
