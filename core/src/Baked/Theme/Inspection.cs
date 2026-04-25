using Baked.Ui;
using Spectre.Console;
using System.Diagnostics;

namespace Baked.Theme;

public class Inspection(StackTrace _stackTrace)
{
    static ComponentPath _lastPath;

    public static void ClearLastPath() =>
        _lastPath = default;

    static object? Evaluate<T>(T result)
    {
        var inspect = Inspect.Current;
        if (inspect is null) { return null; }

        object? target = result is IComponentDescriptor descriptor
            ? descriptor.Schema
            : result;
        if (target is null || !target.GetType().IsAssignableTo(inspect.SchemaType)) { return null; }

        return inspect.Evaluate(target);
    }

    public T Capture<T>(ComponentContext context, T target, Action update) =>
        Capture(context, apply: () => { update(); return target; }, current: target);

    public T Capture<T>(ComponentContext context, Func<T> create) =>
        Capture(context, apply: create);

    T Capture<T>(ComponentContext context, Func<T> apply,
        T? current = default
    )
    {
        var inspect = Inspect.Current;
        if (inspect is null) { return apply(); }
        if (!inspect.Filter(context)) { return apply(); }

        var old = Evaluate(current);
        var result = apply();
        var value = Evaluate(result);
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

            source = $"[link={url}]title[/]";
        }

        Diagnostics.ReportInfo($"{value} ← [magenta]{source}[/]");

        return result;
    }
}