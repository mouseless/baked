using Do.Core;

namespace EventScheduler;

public class SchedulerService
{
    readonly ISystem _system;

    public SchedulerService(ISystem system) => _system = system;

    public DateTime GetNow() => _system.Now;
}