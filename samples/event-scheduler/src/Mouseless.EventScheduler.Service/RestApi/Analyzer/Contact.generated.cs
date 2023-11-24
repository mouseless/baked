// This file will be auto-generated

using Do.Orm;
using Microsoft.AspNetCore.Mvc;

namespace Mouseless.EventScheduler;

[ApiController]
public class ContactController
{
    readonly IServiceProvider _serviceProvider;

    public ContactController(IServiceProvider serviceProvider) =>
        _serviceProvider = serviceProvider;

    [HttpGet]
    [Produces("application/json")]
    [Route("contacts")]
    public List<Contact> All()
    {
        var target = _serviceProvider.GetRequiredService<Contacts>();

        return target.All();
    }

    public record NewRequest(string Name);

    [HttpPost]
    [Produces("application/json")]
    [Route("contacts")]
    public Contact New([FromBody] NewRequest request)
    {
        var target = _serviceProvider.GetRequiredService<Func<Contact>>();

        return target().With(request.Name);
    }

    public record EditRequest(string Name);

    [HttpPatch]
    [Produces("application/json")]
    [Route("contacts/{id}")]
    public Contact Edit([FromRoute] Guid id, [FromBody] EditRequest request)
    {
        var target = _serviceProvider.GetRequiredService<IQueryContext<Contact>>().SingleById(id);

        target.Edit(request.Name);

        return target;
    }
}
