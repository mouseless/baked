using Spectre.Console;

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
//   - First line: "Component paths:"
//   - Second line: "/"
//   - Followed by tree lines using `├ ` / `└ ` connectors and `│ ` / `  `
//     indentation
//   - Connector and indent are colored gray using Spectre.Console
//     `[gray]..[/]`, node name stays default color but should be escaped using
//     `Markup.Escape`
//   - Append full path to the and in `[gray]..[/]` when `includeFullPaths:` is
//     set to true
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
internal static class ComponentPathTreeVisualizer
{
    public static IEnumerable<string> Visualize(IEnumerable<string> paths,
        bool includeFullPaths = false
    )
    {
        var root = new Node(string.Empty);
        var sorted = paths
            .Select(p => p.Trim())
            .Where(p => !string.IsNullOrEmpty(p))
            .Distinct()
            .OrderBy(p => p)
            .ToList();
        foreach (var path in sorted)
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

        var lines = new List<string> { "Component paths:", "/" };
        RenderNode(root, string.Empty, string.Empty, lines, includeFullPaths);

        return lines;
    }

    static void RenderNode(Node node, string indent, string currentPath, List<string> lines, bool includeFullPaths)
    {
        var children = node.Children.Values.ToList();

        for (var i = 0; i < children.Count; i++)
        {
            var child = children[i];
            var isLast = i == children.Count - 1;
            var connector = isLast ? "└ " : "├ ";
            var childPath = $"{currentPath}/{child.Name}";
            var escapedName = Markup.Escape(child.Name);
            var suffix = includeFullPaths ? $" [gray]{Markup.Escape(childPath)}[/]" : string.Empty;

            lines.Add($"{indent}[gray]{connector}[/]{escapedName}{suffix}");

            var childIndent = indent + (isLast ? "[gray]  [/]" : "[gray]│ [/]");
            RenderNode(child, childIndent, childPath, lines, includeFullPaths);
        }
    }

    class Node(string name)
    {
        public string Name { get; } = name;
        public SortedDictionary<string, Node> Children { get; } = new();
    }
}