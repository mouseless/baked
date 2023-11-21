// This file will be auto-generated

using Microsoft.AspNetCore.Mvc;

namespace EventScheduler;

[ApiController]
public class SchedulerController
{
    readonly IServiceProvider _serviceProvider;

    public SchedulerController(IServiceProvider serviceProvider) =>
        _serviceProvider = serviceProvider;

    [HttpGet]
    [Produces("application/json")]
    [Route("schedular/now")]
    public DateTime GetNow()
    {
        var target = _serviceProvider.GetRequiredService<SchedulerService>();

        var result = target.GetNow();

        return result;
    }
}