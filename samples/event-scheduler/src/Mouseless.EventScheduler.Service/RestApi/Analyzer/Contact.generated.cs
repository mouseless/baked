// This file will be auto-generated

using Do.Orm;
using Microsoft.AspNetCore.Mvc;

namespace Mouseless.EventScheduler;

[ApiController]
public class ContactController
{
    [HttpGet]
    [Produces("application/json")]
    [Route("contacts")]
    public List<Contact> All([FromServices] Contacts target)
    {
        return target.All();
    }

    public record NewRequest(string Name);

    [HttpPost]
    [Produces("application/json")]
    [Route("contacts")]
    public Contact New([FromServices] Func<Contact> newTarget, [FromBody] NewRequest request)
    {
        var target = newTarget();

        return target.With(request.Name);
    }

    public record EditRequest(string Name);

    [HttpPatch]
    [Produces("application/json")]
    [Route("contacts/{id}")]
    public Contact Edit([FromServices] IQueryContext<Contact> contactQuery, [FromRoute] Guid id, [FromBody] EditRequest request)
    {
        var target = contactQuery.SingleById(id);

        target.Edit(request.Name);

        return target;
    }
}
