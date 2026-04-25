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
    public static IEnumerable<string> Visualize(IEnumerable<string>? paths)
    {
        var sorted = (paths ?? [])
            .Distinct()
            .OrderBy(p => p)
            .ToList();

        var root = new Node(string.Empty);
        foreach (var path in sorted)
        {
            var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var current = root;
            foreach (var segment in segments)
                current = current.GetOrAdd(segment);
        }

        var lines = new List<string> { "Component paths:", "/" };
        AppendChildren(root, prefix: string.Empty, lines);
        return lines;
    }

    private static void AppendChildren(Node node, string prefix, List<string> lines)
    {
        var children = node.Children.Values.ToList();
        for (var i = 0; i < children.Count; i++)
        {
            var child = children[i];
            var isLast = i == children.Count - 1;

            var connector = isLast ? "└ " : "├ ";
            var escapedName = Markup.Escape(child.Name);
            lines.Add($"{prefix}[gray]{connector}[/]{escapedName}");

            var childPrefix = prefix + (isLast ? "[gray]  [/]" : "[gray]│ [/]");
            AppendChildren(child, childPrefix, lines);
        }
    }

    private class Node(string name)
    {
        public string Name { get; } = name;
        public SortedDictionary<string, Node> Children { get; } = new();

        public Node GetOrAdd(string segment) =>
            Children.TryGetValue(segment, out var child)
                ? child
                : Children[segment] = new Node(segment);
    }
}