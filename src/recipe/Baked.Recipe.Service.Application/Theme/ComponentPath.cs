using Humanizer;
using System.Text.RegularExpressions;

namespace Baked.Theme;

public readonly record struct ComponentPath(string Value)
{
    public ComponentPath(params object[] paths)
        : this($"/{Join(paths)}") { }

    static string Join(object[] paths) =>
        paths.Select(p => $"{p}".Kebaberize()).Join('/');

    public ComponentPath Drill(params object[] paths) =>
        this with { Value = $"{Value}/{Join(paths)}" };

    public bool Is(params object[] paths) =>
        Value == $"/{Join(paths)}";

    public bool Matches(Regex regex) =>
        regex.IsMatch(Value);

    public bool Contains(params object[] paths) =>
        Value.Contains($"/{Join(paths)}/");

    public bool EndsWith(params object[] paths) =>
        Value.EndsWith($"/{Join(paths)}");

    public override string ToString() =>
        Value;
}