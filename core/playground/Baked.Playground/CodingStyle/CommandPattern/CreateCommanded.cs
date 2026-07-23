using Microsoft.Extensions.Logging;

namespace Baked.Playground.CodingStyle.CommandPattern;

public class CreateCommanded(ILogger<CreateCommanded> _logger)
{
    public string Execute(List<CommandedDescriptor> descriptors)
    {
        _logger.LogInformation($"Creating {descriptors.Join(", ")}");

        return descriptors.Select(d => d.Name).Join(':');
    }
}