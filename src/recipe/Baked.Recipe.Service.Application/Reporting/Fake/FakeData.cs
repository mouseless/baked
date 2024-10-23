using System.Text.RegularExpressions;

namespace Baked.Reporting.Fake;

public record FakeData(
    Dictionary<string, string?>? Parameters,
    List<Dictionary<string, object?>> Result
)
{
    public bool Matches(Dictionary<string, object> parameters)
    {
        if (Parameters is null) { return true; }

        foreach (var (key, pattern) in Parameters)
        {
            if (!parameters.ContainsKey(key))
            {
                return false;
            }

            if (pattern is null) { continue; }
            if (Regex.IsMatch($"{parameters[key]}", pattern)) { continue; }

            return false;
        }

        return true;
    }
}