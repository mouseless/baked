using Microsoft.Extensions.Logging;

namespace Baked.Test.CodingStyle.CommandPattern;

public class SyncBulkCommand(ILogger<SyncBulkCommand> _logger)
{
    public string Execute(List<BulkDescriptor> descriptors)
    {
        _logger.LogInformation($"Syncing {descriptors.Join(", ")}");

        return descriptors.Select(d => d.Name).Join(':');
    }
}