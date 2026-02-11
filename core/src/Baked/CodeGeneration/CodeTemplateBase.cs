namespace Baked.CodeGeneration;

public abstract class CodeTemplateBase : ICodeTemplate
{
    protected abstract IEnumerable<string> Render();

    protected string Join(string separator, params IEnumerable<string> statements) =>
        statements
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Join(separator ?? Environment.NewLine);

    protected string ForEach<T>(IEnumerable<T> items, Func<T, string> body,
        string? separator = default,
        int indentCorrection = 0
    ) => items
      .Select(i => body(i).Split(Environment.NewLine).Join($"{Environment.NewLine}{new string(' ', indentCorrection * 4)}"))
      .Join($"{separator ?? Environment.NewLine}{new string(' ', indentCorrection * 4)}");

    protected string If(bool condition, Func<string> then,
        Func<string>? @else = default
    ) => condition ? then() : @else?.Invoke() ?? string.Empty;

    IEnumerable<string> ICodeTemplate.Render() => Render();
}