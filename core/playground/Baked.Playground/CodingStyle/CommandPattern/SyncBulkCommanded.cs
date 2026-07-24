using Microsoft.Extensions.Logging;

namespace Baked.Playground.CodingStyle.CommandPattern;

public class SyncBulkCommanded(ILogger<SyncBulkCommanded> _logger)
{
    public string Execute(List<BulkCommandedDescriptor> descriptors)
    {
        _logger.LogInformation($"Syncing {descriptors.Join(", ")}");

        return descriptors.Select(d => d.Name).Join(':');
    }
}