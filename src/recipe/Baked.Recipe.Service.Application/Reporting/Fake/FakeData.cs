using System.Text.RegularExpressions;

namespace Baked.Reporting.Fake;

public record FakeData(
    Dictionary<string, string?>? Parameters,
    List<Dictionary<string, object?>> Result
)
{
    public bool Matches(Dictionary<string, object?> parameters)
    {
        if (Parameters is null) { return true; }

        foreach (var (key, pattern) in Parameters)
        {
            if (!parameters.TryGetValue(key, out var value))
            {
                return false;
            }

            if (pattern is null)
            {
                if (value is null) { continue; }
                return false;
            }

            if (Regex.IsMatch(value?.ToString() ?? string.Empty, pattern))
            {
                continue;
            }

            return false;
        }

        return true;
    }
}