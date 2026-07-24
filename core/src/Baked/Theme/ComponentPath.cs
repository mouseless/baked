using Humanizer;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Baked.Theme;

public readonly record struct ComponentPath(string Value)
{
    static readonly List<string> _paths = [];
    static readonly ConcurrentDictionary<string, Regex> _regexCache = new();
    static readonly ConcurrentDictionary<string, string> _kebabCache = new();

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

    static string Kebab(object value) =>
        _kebabCache.GetOrAdd(
            value as string ?? value.ToString() ?? string.Empty,
            static v => v.Kebaberize()
        );

    static string Join(object[] paths) =>
        paths.Select(Kebab).Join('/');

    static string Regexify(string joined) =>
        joined
            .Replace("**", "__")
            .Replace("*", "[^/]*")
            .Replace("__", ".*");

    static Regex GetRegex(string pattern) =>
        _regexCache.GetOrAdd(pattern, static p => new Regex(p));

    enum Kind
    {
        Full,
        Start,
        Middle,
        End
    }

    bool Matches(object[] paths, Kind kind)
    {
        var joined = Join(paths);

        if (joined.Contains('*'))
        {
            var body = Regexify(joined);
            var pattern = kind switch
            {
                Kind.Full => $"^/{body}$",
                Kind.Start => $"^/{body}",
                Kind.Middle => $"/{body}/",
                Kind.End => $"/{body}$",
                _ => $"/{body}$",
            };
            return GetRegex(pattern).IsMatch(Value);
        }

        var target = $"/{joined}";
        return kind switch
        {
            Kind.Full => Value == target,
            Kind.Start => Value.StartsWith(target, StringComparison.Ordinal),
            Kind.Middle => Value.Contains($"{target}/", StringComparison.Ordinal),
            Kind.End => Value.EndsWith(target, StringComparison.Ordinal),
            _ => Value.EndsWith(target, StringComparison.Ordinal),
        };
    }

    public ComponentPath Drill(params object[] paths) =>
        this with { Value = $"{Value}/{Join(paths)}" };

    public bool IsMatch(Regex regex) =>
        regex.IsMatch(Value);

    public bool Is(params object[] paths) =>
        Matches(paths, Kind.Full);

    public bool StartsWith(params object[] paths) =>
        Matches(paths, Kind.Start);

    public bool Contains(params object[] paths) =>
        Matches(paths, Kind.Middle);

    public bool EndsWith(params object[] paths) =>
        Matches(paths, Kind.End);

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