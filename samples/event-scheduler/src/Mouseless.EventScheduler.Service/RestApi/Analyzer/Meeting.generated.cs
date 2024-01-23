// This file will be auto-generated

using Do.Orm;
using Microsoft.AspNetCore.Mvc;

namespace Mouseless.EventScheduler;

[ApiController]
public class MeetingController
{
    [HttpGet]
    [Produces("application/json")]
    [Route("meetings")]
    public List<Meeting> By([FromServices] Meetings target, [FromQuery] DateTime? before, [FromQuery] DateTime? after)
    {
        return target.By(before, after);
    }

    [HttpGet]
    [Produces("application/json")]
    [Route("meetings/{id}")]
    public Meeting Get([FromServices] IQueryContext<Meeting> meetingQuery, [FromRoute] Guid id)
    {
        return meetingQuery.SingleById(id);
    }

    public record NewRequest(string Name, DateTime Date);

    [HttpPost]
    [Produces("application/json")]
    [Route("meetings")]
    public Meeting New([FromServices] Func<Meeting> newTarget, [FromBody] NewRequest request)
    {
        var target = newTarget();

        return target.With(request.Name, request.Date);
    }

    [HttpGet]
    [Produces("application/json")]
    [Route("meetings/{id}/contacts")]
    public List<Contact> GetContacts([FromServices] IQueryContext<Meeting> meetingQuery, [FromRoute] Guid id)
    {
        var target = meetingQuery.SingleById(id);

        return target.GetContacts();
    }

    public record AddContactRequest(Guid ContactId);

    [HttpPost]
    [Produces("application/json")]
    [Route("meetings/{id}/contacts")]
    public void AddContact([FromServices] IQueryContext<Meeting> meetingQuery, [FromServices] IQueryContext<Contact> contactQuery, [FromRoute] Guid id, [FromBody] AddContactRequest request)
    {
        var target = meetingQuery.SingleById(id);

        target.AddContact(contactQuery.SingleById(request.ContactId));
    }

    [HttpDelete]
    [Produces("application/json")]
    [Route("meetings/{id}")]
    public void Delete([FromServices] IQueryContext<Meeting> meetingQuery, [FromRoute] Guid id)
    {
        var target = meetingQuery.SingleById(id);

        target.Delete();
    }
}