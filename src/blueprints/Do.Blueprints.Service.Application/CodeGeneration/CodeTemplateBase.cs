namespace Do.CodeGeneration;

public abstract class CodeTemplateBase : ICodeTemplate
{
    protected abstract IEnumerable<string> Render();

    protected string ForEach<T>(IEnumerable<T> items, Func<T, string> body,
        string? separator = default
    ) => string.Join(separator ?? Environment.NewLine, items.Select(body));

    protected string If(bool condition, Func<string> then, Func<string>? @else = default) =>
        condition ? then() : @else?.Invoke() ?? string.Empty;

    IEnumerable<string> ICodeTemplate.Render() => Render();
}