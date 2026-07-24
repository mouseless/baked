using Microsoft.Extensions.Logging;

namespace Baked.Playground.CodingStyle.CommandPattern;

public class ClearCommanded(ILogger<ClearCommanded> _logger)
{
    string? _initParam = default!;

    public ClearCommanded With(string? initParam)
    {
        _initParam = initParam;

        return this;
    }

    public string Execute(string? executeParam)
    {
        _logger.LogInformation($"Deleting using params '{_initParam}' and '{executeParam}'");

        return $"{_initParam}:{executeParam}";
    }
}