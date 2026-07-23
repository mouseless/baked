using Microsoft.Extensions.Logging;

namespace Baked.Playground.CodingStyle.CommandPattern;

public class SyncCommanded(ILogger<SyncCommanded> _logger)
{
    public string Execute(List<CommandedDescriptor> descriptors)
    {
        _logger.LogInformation($"Syncing {descriptors.Join(", ")}");

        return descriptors.Select(d => d.Name).Join(':');
    }
}