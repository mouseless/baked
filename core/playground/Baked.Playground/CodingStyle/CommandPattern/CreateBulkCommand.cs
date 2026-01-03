using Microsoft.Extensions.Logging;

namespace Baked.Playground.CodingStyle.CommandPattern;

public class CreateBulkCommand(ILogger<CreateBulkCommand> _logger)
{
    public string Execute(List<BulkDescriptor> descriptors)
    {
        _logger.LogInformation($"Creating {descriptors.Join(", ")}");

        return descriptors.Select(d => d.Name).Join(':');
    }
}