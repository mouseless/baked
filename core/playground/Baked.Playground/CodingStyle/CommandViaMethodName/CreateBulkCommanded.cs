using Microsoft.Extensions.Logging;

namespace Baked.Playground.CodingStyle.CommandViaMethodName;

public class CreateBulkCommanded(ILogger<CreateBulkCommanded> _logger)
{
    public string Execute(List<BulkCommandedDescriptor> descriptors)
    {
        _logger.LogInformation($"Creating {descriptors.Join(", ")}");

        return descriptors.Select(d => d.Name).Join(':');
    }
}