// This file will be auto-generated

using Do.Orm;
using Microsoft.AspNetCore.Mvc;

namespace Do.Test.RestApi.Analyzer;

[ApiController]
public class EntityController
{
    private IServiceProvider _serviceProvider;

    public EntityController(IServiceProvider serviceProvider) =>
        _serviceProvider = serviceProvider;

    [HttpGet]
    [Route("entities")]
    public List<Entity> By([FromQuery] string @string = default)
    {
        var target = _serviceProvider.GetRequiredService<Entities>();

        return target.By(@string);
    }

    [HttpGet]
    [Route("entities/{id}")]
    public Entity Get(Guid id)
    {
        var target = _serviceProvider.GetRequiredService<IQueryContext<Entity>>();

        return target.SingleById(id);
    }

    public record NewRequest(string String, int Int32, Uri Uri, object Dynamic);

    [HttpPost]
    [Route("entities")]

    public Entity New([FromBody] NewRequest request)
    {
        var target = _serviceProvider.GetRequiredService<Entity>();

        return target.With(request.String, request.Int32, request.Uri, request.Dynamic);
    }

    [HttpDelete]
    [Route("entities/{id}")]
    public void Delete([FromRoute] Guid id)
    {
        var target = _serviceProvider.GetRequiredService<IQueryContext<Entity>>();

        target.SingleById(id).Delete();
    }

    public record UpdateRequest(string String, int Int32, Uri Uri, object Dynamic);

    [HttpPut]
    [Route("entities/{id}")]
    public void Update([FromRoute] Guid id, [FromBody] UpdateRequest request)
    {
        var target = _serviceProvider.GetRequiredService<IQueryContext<Entity>>();

        target.SingleById(id).Update(request.String, request.Int32, request.Uri, request.Dynamic);
    }
}
