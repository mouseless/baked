// This file will be auto-generated

using Microsoft.AspNetCore.Mvc;

namespace EventScheduler;

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

        var result = target.By(before, after);

        return result;
    }

    public record NewMeetingRequest(string Name, DateTime Date);

    [HttpPost]
    [Produces("application/json")]
    [Route("meetings")]
    public Meeting New([FromBody] NewMeetingRequest request)
    {
        var newTarget = _serviceProvider.GetRequiredService<Func<Meeting>>();

        var result = newTarget().With(request.Name, request.Date);

        return result;
    }
}