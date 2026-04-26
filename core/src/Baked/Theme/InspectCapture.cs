using Baked.Core;
using Baked.Ui;
using Newtonsoft.Json;
using Spectre.Console;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Baked.Theme;

internal class InspectCapture<T>
{
    readonly Inspect _inspect;
    readonly StackTrace _stackTrace;
    readonly ComponentContext _context;
    readonly Func<T> _apply;
    readonly bool _create;
    readonly T? _givenTarget;

    public InspectCapture(Inspect inspect, StackTrace stackTrace, ComponentContext context, Func<T> create)
        : this(inspect, stackTrace, context, create, true) { }

    public InspectCapture(Inspect inspect, StackTrace stackTrace, ComponentContext context, Action update, T target)
        : this(inspect, stackTrace, context, () => { update(); return target; }, false, target) { }

    InspectCapture(Inspect inspect, StackTrace stackTrace, ComponentContext context, Func<T> apply, bool create,
        T? givenTarget = default
    )
    {
        _inspect = inspect;
        _stackTrace = stackTrace;
        _context = context;
        _apply = apply;
        _create = create;
        _givenTarget = givenTarget;
    }

    string Property => _inspect.Expression.StripLambdaFromASingleMemberAccessExpression();

    public T Execute()
    {
        TryEvaluate(_givenTarget, out var previousValue, out var _);
        var target = _apply();
        if (TryEvaluate(target, out var value, out var type) && _create)
        {
            Diagnostics.ReportInfo($"[lightskyblue3_1]<{type.GetName(includeDeclaringTypes: true)}>[/] [gray]{_context.Path}[/]");
        }

        if (Equals(value, previousValue)) { return target; }

        var source = TryFindFeatureSource(out var featureSource)
            ? $"[magenta]{featureSource}[/]"
            : $"[magenta]<unknown>[/]{Environment.NewLine}[gray]{Markup.Escape($"{_stackTrace}")}[/]";
        Diagnostics.ReportInfo($"  [gray]{Property}:[/] {Markup.Escape(FormatValue(value))} ← {source}");

        return target;
    }

    bool TryEvaluate(T? target, out object? value, [NotNullWhen(true)] out Type? concreteTypeOfTarget)
    {
        value = null;
        concreteTypeOfTarget = null;

        object? targetObject = target is IComponentDescriptor descriptor
            ? descriptor.Schema
            : target;
        if (targetObject is null || !targetObject.GetType().IsAssignableTo(_inspect.SchemaType)) { return false; }

        value = _inspect.Evaluate(targetObject);
        concreteTypeOfTarget = targetObject.GetType();

        return true;
    }

    bool TryFindFeatureSource([NotNullWhen(true)] out string? source)
    {
        source = null;

        var frames = _stackTrace.GetFrames();
        var featureFrame =
            frames.FirstOrDefault(f => f.GetMethod()?.ReflectedType?.DeclaringType?.Name.EndsWith("Feature") == true) ??
            frames.FirstOrDefault(f => f.GetMethod()?.DeclaringType?.Name.EndsWith("Feature") == true);
        if (featureFrame is null) { return false; }

        source =
            featureFrame.GetMethod()?.ReflectedType?.DeclaringType?.Name ??
            featureFrame.GetMethod()?.DeclaringType?.Name ??
            string.Empty;

        var fileName = featureFrame.GetFileName();
        if (fileName is null) { return true; }

        var title = source;
        var url = new Uri(fileName).AbsoluteUri;
        var lineNumber = featureFrame.GetFileLineNumber();
        if (lineNumber > 0)
        {
            title += $":{lineNumber}";
        }

        source = $"[link={url}]{title}[/]";

        return true;
    }

    static string FormatValue(object? value) =>
        value is string || value?.GetType().SkipNullable().IsValueType == true
            ? $"{value}"
            : JsonConvert.SerializeObject(value, Formatting.Indented);
}