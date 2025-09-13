using Humanizer;
using System.Text.RegularExpressions;

namespace Baked.Theme;

public readonly record struct ComponentPath(string Value)
{
    public ComponentPath(params object[] paths)
        : this($"/{Join(paths)}") { }

    static string Join(object[] paths,
        bool regexify = false
    )
    {
        var result = paths.Select(p => $"{p}".Kebaberize()).Join('/');

        if (regexify)
        {
            result = result
                .Replace("**", "__")
                .Replace("*", "[^/]*")
                .Replace("__", ".*");
        }

        return result;
    }

    public ComponentPath Drill(params object[] paths) =>
        this with { Value = $"{Value}/{Join(paths)}" };

    public bool Is(params object[] paths) =>
        Value == $"/{Join(paths)}";

    public bool IsMatch(Regex regex) =>
        regex.IsMatch(Value);

    public bool StartsWith(params object[] paths) =>
        Regex.IsMatch(Value, $"^/{Join(paths, regexify: true)}");

    public bool Contains(params object[] paths) =>
        Regex.IsMatch(Value, $"/{Join(paths, regexify: true)}/");

    public bool EndsWith(params object[] paths) =>
        Regex.IsMatch(Value, $"/{Join(paths, regexify: true)}$");

    public override string ToString() =>
        Value;
}