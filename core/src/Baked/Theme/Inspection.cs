using Baked.Ui;
using System.Diagnostics;

namespace Baked.Theme;

public class Inspection(StackTrace _stackTrace)
{
    const string Esc = "\x1b";
    const string LightMagenta = $"{Esc}[95m";
    const string Gray = $"{Esc}[90m";
    const string Reset = $"{Esc}[0m";

    static string FileLink(string title, string url) =>
        $"{Esc}]8;;{url}{Esc}\\{title}{Esc}]8;;{Esc}\\";

    static string NullSafe(object? value) =>
        value is null
            ? $"{Gray}null{Reset}"
            : $"{value}";

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
        if (target?.GetType() != inspect.SchemaType) { return result; }

        value = inspect.Evaluate(target);
        if (Equals(value, old)) { return result; }

        if (_lastPath != context.Path)
        {
            _lastPath = context.Path;
            Diagnostics.ReportInfo($"{Gray}{_lastPath}{Reset}");
        }

        var frames = _stackTrace.GetFrames();
        var featureFrame = frames.FirstOrDefault(f =>
            f.GetMethod()?.ReflectedType?.Name == "<>c" &&
            f.GetMethod()?.ReflectedType?.DeclaringType?.Name.EndsWith("Feature") == true
        );

        if (featureFrame is null)
        {
            Diagnostics.ReportInfo($"{NullSafe(value)} ⤌ {LightMagenta}<unknown>{Reset}");

            return result;
        }

        var source = featureFrame.GetMethod()?.ReflectedType?.DeclaringType?.Name ?? string.Empty;
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

        Diagnostics.ReportInfo($"{NullSafe(value)} ← {LightMagenta}{source}{Reset}");

        return result;
    }
}