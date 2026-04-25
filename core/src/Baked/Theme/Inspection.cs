using Baked.Ui;
using Newtonsoft.Json;
using Spectre.Console;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Baked.Theme;

public class Inspection(StackTrace _stackTrace)
{
    static bool TryEvaluate<T>(T result, out object? value, [NotNullWhen(true)] out Type? type)
    {
        value = null;
        type = null;
        var inspect = Inspect.Current;
        if (inspect is null) { return false; }

        object? target = result is IComponentDescriptor descriptor
            ? descriptor.Schema
            : result;
        if (target is null || !target.GetType().IsAssignableTo(inspect.SchemaType)) { return false; }

        value = inspect.Evaluate(target);
        type = target.GetType();

        return true;
    }

    public T Capture<T>(ComponentContext context, Func<T> create) =>
        Capture(context, create, create: true);

    public T Capture<T>(ComponentContext context, T target, Action update) =>
        Capture(context, () => { update(); return target; }, create: false, current: target);

    T Capture<T>(ComponentContext context, Func<T> apply,
        bool create = false,
        T? current = default
    )
    {
        var inspect = Inspect.Current;
        if (inspect is null) { return apply(); }
        if (!inspect.Filter(context)) { return apply(); }

        TryEvaluate(current, out var old, out var _);
        var result = apply();
        if (TryEvaluate(result, out var value, out var schemaType) && create)
        {
            Diagnostics.ReportInfo($"[lightskyblue3_1]<{schemaType.Name}>[/] [gray]{context.Path}[/]");
        }

        if (Equals(value, old)) { return result; }

        string source;
        var frames = _stackTrace.GetFrames();
        var featureFrame =
            frames.FirstOrDefault(f => f.GetMethod()?.ReflectedType?.DeclaringType?.Name.EndsWith("Feature") == true) ??
            frames.FirstOrDefault(f => f.GetMethod()?.DeclaringType?.Name.EndsWith("Feature") == true);
        if (featureFrame is null)
        {
            source = $"[magenta]<unknown>[/]{Environment.NewLine}[gray]{Markup.Escape($"{_stackTrace}")}[/]";
        }
        else
        {
            source =
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

                source = $"[link={url}]{title}[/]";
            }

            source = $"[magenta]{source}[/]";
        }

        var valueString = value is string || value?.GetType().SkipNullable().IsValueType == true
            ? $"{value}"
            : JsonConvert.SerializeObject(value, Formatting.Indented);
        Diagnostics.ReportInfo($"  {Markup.Escape(valueString)} ← {source}");

        return result;
    }
}