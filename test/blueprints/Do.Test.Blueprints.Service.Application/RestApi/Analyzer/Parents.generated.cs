using Do.Orm;
using Microsoft.AspNetCore.Mvc;

namespace Do.Test.RestApi.Analyzer;

[ApiController]
public class ParentEntitiesController
{
    public record AllRequest(
        bool Reverse = false
    );

    [HttpGet]
    [Route("parents")]
    public List<Parent> All([FromServices] Parents target, [FromQuery] AllRequest request)
    {
        return target.All();
    }

    [HttpGet]
    [Route("parents/{id}")]
    public Parent Get([FromServices] IQueryContext<Parent> parentQuery, Guid id)
    {
        return parentQuery.SingleById(id);
    }

    [HttpGet]
    [Route("parents/{id}/children")]
    public List<Child> GetChildren([FromServices] IQueryContext<Parent> parentQuery, [FromRoute] Guid id)
    {
        var target = parentQuery.SingleById(id);

        return target.GetChildren();
    }

    public record AddChildRequest();

    [HttpPost]
    [Route("parents/{id}/children")]
    public void AddChild([FromServices] IQueryContext<Parent> parentQuery, [FromRoute] Guid id, [FromBody] AddChildRequest request)
    {
        var target = parentQuery.SingleById(id);

        target.AddChild();
    }
}
