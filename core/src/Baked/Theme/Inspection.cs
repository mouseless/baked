using Baked.Ui;
using Spectre.Console;
using System.Diagnostics;

namespace Baked.Theme;

public class Inspection(StackTrace _stackTrace)
{
    static string FileLink(string title, string url) =>
        $"[link={url}]{title}[/]";

    static ComponentPath _lastPath;

    public object? Evaluate<T>(T result)
    {
        var inspect = Inspect.Current;
        if (inspect is null) { return null; }

        object? target = result is IComponentDescriptor descriptor
            ? descriptor.Schema
            : result;
        if (target?.GetType() != inspect.SchemaType) { return null; }

        return inspect.Evaluate(target);
    }

    public T Capture<T>(ComponentContext context, T result,
        object? old = null
    ) => Capture(context, result, out var _, old: old);

    public T Capture<T>(ComponentContext context, T result, out object? value,
        object? old = null
    )
    {
        value = null;
        var inspect = Inspect.Current;

        if (inspect is null) { return result; }
        if (!inspect.Filter(context)) { return result; }

        object? target = result is IComponentDescriptor descriptor
            ? descriptor.Schema
            : result;
        if (target?.GetType().IsAssignableTo(inspect.SchemaType) != true) { return result; }

        value = inspect.Evaluate(target);
        if (Equals(value, old)) { return result; }

        if (_lastPath != context.Path)
        {
            _lastPath = context.Path;
            Diagnostics.ReportInfo($"[gray]{_lastPath}[/]");
        }

        var frames = _stackTrace.GetFrames();
        var featureFrame =
            frames.FirstOrDefault(f => f.GetMethod()?.ReflectedType?.DeclaringType?.Name.EndsWith("Feature") == true) ??
            frames.FirstOrDefault(f => f.GetMethod()?.DeclaringType?.Name.EndsWith("Feature") == true);
        if (featureFrame is null)
        {
            Diagnostics.ReportInfo($"{value} ⤌ [magenta]<unknown>[/]{Environment.NewLine}[gray]{Markup.Escape($"{_stackTrace}")}[/]");

            return result;
        }

        var source =
            featureFrame.GetMethod()?.ReflectedType?.DeclaringType?.Name ??
            featureFrame.GetMethod()?.DeclaringType?.Name ??
            string.Empty;
        var fileName = featureFrame.GetFileName();
        if (fileName is not null)
        {
            var title = source;
            var url = new Uri(fileName).AbsoluteUri;
            var lineNumber = featureFrame.GetFileLineNumber();
            if (lineNumber > 0)
            {
                title += $":{lineNumber}";
            }

            source = FileLink(title, url);
        }

        Diagnostics.ReportInfo($"{value} ← [magenta]{source}[/]");

        return result;
    }
}