namespace Baked.Theme;

// AI-GEN (Claude - Sonnet 4.6)
//
// Write an internal C# class called `ComponentPathTreeVisualizer` with a
// single public static method:
//
//   public static IEnumerable<string> Visualize(IEnumerable<string> paths)
//
// It takes Unix-style path strings (e.g.
// `/page/form-sample/new-parent/form-page/action`), builds a tree, and
// returns it as lines ready to print — no direct `Console` calls.
//
// Output format:
//   - First line: "Drilled component paths:"
//   - Second line: "/"
//   - Followed by tree lines using `├ ` / `└ ` connectors and `│ ` / `  `
//     indentation
//   - Connector and indent are colored dark gray via ANSI `\x1b[90m]` /
//     `\x1b[0m]`, node name stays default color
//
// Coding style preferences:
//   - Private nested classes and helpers — internalize everything,
//     expose only `Visualize`
//   - Primary constructors for simple internal types
//   - `string.Empty` over `""`
//   - `var` for locals
//   - `SortedDictionary` for children so siblings are always
//     alphabetically ordered
//   - Deduplicate and sort input before processing
//   - Nullable prefix parameter with null-coalescing default rather
//     than overloading
internal class ComponentPathTreeVisualizer
{
    class Node(string name)
    {
        public string Name { get; } = name;
        public SortedDictionary<string, Node> Children { get; } = new();
    }

    static readonly string _gray = "\x1b[90m";
    static readonly string _reset = "\x1b[0m";

    public static IEnumerable<string> Visualize(IEnumerable<string> paths)
    {
        paths = paths.Order().Distinct();

        var result = new List<string>();
        var root = new Node("/");
        foreach (var path in paths)
        {
            var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var current = root;

            foreach (var segment in segments)
            {
                if (!current.Children.TryGetValue(segment, out var child))
                {
                    child = new Node(segment);
                    current.Children[segment] = child;
                }

                current = child;
            }
        }

        result.Add("Drilled component paths:");
        result.Add(root.Name);
        result.AddRange(PrintChildren(root));

        return result;
    }

    static IEnumerable<string> PrintChildren(Node node,
        string? prefix = default
    )
    {
        prefix ??= string.Empty;

        var result = new List<string>();
        var children = node.Children.Values.ToList();

        for (int i = 0; i < children.Count; i++)
        {
            var child = children[i];
            var isLast = i == children.Count - 1;

            var connector = isLast ? "└ " : "├ ";
            var childPrefix = isLast ? "  " : "│ ";

            result.Add($"{_gray}{prefix}{connector}{_reset}{child.Name}");
            result.AddRange(PrintChildren(child, $"{prefix}{childPrefix}"));
        }

        return result;
    }
}