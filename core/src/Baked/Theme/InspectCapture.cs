using Baked.Core;
using Baked.Ui;
using Baked.Ui.Configuration;
using Newtonsoft.Json;
using Spectre.Console;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Baked.Theme;

internal class InspectCapture<T>
{
    readonly Inspect _inspect;
    readonly StackTrace _stackTrace;
    readonly Func<T> _apply;
    readonly Action<Type>? _reportCreate;
    readonly T? _givenTarget;

    public InspectCapture(Inspect inspect, StackTrace stackTrace, Func<T> create, Action<Type> reportCreate)
        : this(inspect, stackTrace, create, reportCreate, default) { }

    public InspectCapture(Inspect inspect, StackTrace stackTrace, Action update, T target)
        : this(inspect, stackTrace, () => { update(); return target; }, null, target) { }

    InspectCapture(Inspect inspect, StackTrace stackTrace, Func<T> apply, Action<Type>? reportCreate, T? givenTarget)
    {
        _inspect = inspect;
        _stackTrace = stackTrace;
        _apply = apply;
        _reportCreate = reportCreate;
        _givenTarget = givenTarget;
    }

    string Property => _inspect.Expression.StripLambdaFromASingleMemberAccessExpression();

    public T Execute()
    {
        TryEvaluate(_givenTarget, out var previousValue, out var _);
        var target = _apply();
        if (TryEvaluate(target, out var value, out var type) && _reportCreate is not null)
        {
            _reportCreate(type);
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
        if (targetObject is null || !targetObject.GetType().IsAssignableTo(_inspect.TargetType)) { return false; }

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

    static readonly JsonSerializerSettings _serializerSettings = new()
    {
        ContractResolver = new AttributeAwareCamelCasePropertyNamesContractResolver()
    };

    static string FormatValue(object? value) =>
        value is string || value?.GetType().SkipNullable().IsValueType == true
            ? $"{value}"
            : JsonConvert.SerializeObject(value, Formatting.Indented, _serializerSettings);
}