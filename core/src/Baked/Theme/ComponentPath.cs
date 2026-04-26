using Humanizer;
using System.Text.RegularExpressions;

namespace Baked.Theme;

public readonly record struct ComponentPath(string Value)
{
    static readonly List<string> _paths = [];

    internal static void AddPath(ComponentPath path) =>
        _paths.Add(path.Value);

    internal static IEnumerable<string> GetPaths() =>
        _paths.AsReadOnly();

    internal static string GetPathsAsTree(Debug debug) =>
        ComponentPathTreeVisualizer
            .Visualize(
                _paths
                    .Select(p => new ComponentPath(p))
                    .Where(p => debug.Matches(p))
                    .Select(p => p.Value)
                    .ToList()
                    .AsReadOnly(),
                includeFullPaths: debug.IncludeFullPaths
            )
            .Join(Environment.NewLine);

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

    public bool IsMatch(Regex regex) =>
        regex.IsMatch(Value);

    public bool Is(params object[] paths) =>
        IsMatch(new($"^/{Join(paths, regexify: true)}$"));

    public bool StartsWith(params object[] paths) =>
        IsMatch(new($"^/{Join(paths, regexify: true)}"));

    public bool Contains(params object[] paths) =>
        IsMatch(new($"/{Join(paths, regexify: true)}/"));

    public bool EndsWith(params object[] paths) =>
        IsMatch(new($"/{Join(paths, regexify: true)}$"));

    public IEnumerable<string> GetParts() =>
        Value.Trim('/').Split('/');

    public override string ToString() =>
        Value;

    public class Debug
    {
        public Func<ComponentPath, bool> Filter { get; init; } = _ => true;
        public bool IncludeFullPaths { get; init; }

        public bool Matches(ComponentPath path) =>
            Filter(path);

        public static implicit operator Debug(bool value) =>
            new() { Filter = _ => value };
    }
}