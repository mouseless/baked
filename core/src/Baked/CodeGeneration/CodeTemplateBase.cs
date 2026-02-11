namespace Baked.CodeGeneration;

public abstract class CodeTemplateBase : ICodeTemplate
{
    protected virtual int GlobalIndentation => 4;

    protected abstract IEnumerable<string> Render();

    IEnumerable<string> ICodeTemplate.Render() =>
        Render().Select(code =>
            code
                .Split(Environment.NewLine)
                .Select(line =>
                    line.StartsWith(new string(' ', GlobalIndentation))
                        ? line[GlobalIndentation..]
                        : line
                )
                .Join(Environment.NewLine)
        );

    protected string Join(string separator, params IEnumerable<string> statements) =>
        statements
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Join(separator ?? Environment.NewLine);

    protected string ForEach<T>(IEnumerable<T> items, Func<T, string> body,
        string? separator = default,
        int indentation = 0
    ) => items
        .Select(i => AddIndentation(body(i), indentation))
        .Join($"{separator ?? Environment.NewLine}{new string(' ', indentation * 4)}");

    protected string If(bool condition, Func<string> then,
        Func<string>? @else = default,
        int indentation = 0
    ) => AddIndentation(condition ? then() : @else?.Invoke() ?? string.Empty, indentation);

    string AddIndentation(string lines, int level)
    {
        if (level == 0) { return lines; }

        return lines
            .Split(Environment.NewLine)
            .Join($"{Environment.NewLine}{new string(' ', level * 4)}");
    }
}