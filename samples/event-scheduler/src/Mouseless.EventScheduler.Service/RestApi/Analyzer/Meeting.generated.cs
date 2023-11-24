// This file will be auto-generated

using Do.Orm;
using Microsoft.AspNetCore.Mvc;

namespace Mouseless.EventScheduler;

[ApiController]
public class MeetingController
{
    readonly IServiceProvider _serviceProvider;

    public MeetingController(IServiceProvider serviceProvider) =>
        _serviceProvider = serviceProvider;

    [HttpGet]
    [Produces("application/json")]
    [Route("meetings")]
    public List<Meeting> By([FromQuery] DateTime? before, [FromQuery] DateTime? after)
    {
        var target = _serviceProvider.GetRequiredService<Meetings>();

        return target.By(before, after);
    }

    [HttpGet]
    [Produces("application/json")]
    [Route("meetings/{id}")]
    public Meeting Get([FromRoute] Guid id)
    {
        var target = _serviceProvider.GetRequiredService<IQueryContext<Meeting>>();

        return  target.SingleById(id);
    }

    public record NewRequest(string Name, DateTime Date);

    [HttpPost]
    [Produces("application/json")]
    [Route("meetings")]
    public Meeting New([FromBody] NewRequest request)
    {
        var target = _serviceProvider.GetRequiredService<Func<Meeting>>();

        return target().With(request.Name, request.Date);
    }

    [HttpGet]
    [Produces("application/json")]
    [Route("meetings/{id}/contacts")]
    public List<Contact> GetContacts([FromRoute] Guid id)
    {
        var target = _serviceProvider.GetRequiredService<IQueryContext<Meeting>>().SingleById(id);

        return target.GetContacts();
    }

    public record AddContactRequest(Guid ContactId);

    [HttpPost]
    [Produces("application/json")]
    [Route("meetings/{id}/contacts")]
    public void AddContact([FromRoute] Guid id, [FromBody] AddContactRequest request)
    {
        var target = _serviceProvider.GetRequiredService<IQueryContext<Meeting>>().SingleById(id);

        target.AddContact(_serviceProvider.GetRequiredService<IQueryContext<Contact>>().SingleById(request.ContactId));
    }

    [HttpDelete]
    [Produces("application/json")]
    [Route("meetings/{id}")]
    public void Delete([FromRoute] Guid id)
    {
        var target = _serviceProvider.GetRequiredService<IQueryContext<Meeting>>();

        target.SingleById(id).Delete();
    }
}