using Baked.Core;
using Baked.Runtime;
using Newtonsoft.Json;
using Spectre.Console;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Baked.Domain.Inspection;

internal class Capture<T>
{
    static JsonSerializerSettings SerializerSettings { get; } = new()
    {
        ContractResolver = new AttributeAwareCamelCasePropertyNamesContractResolver(),
        NullValueHandling = NullValueHandling.Ignore,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    static string FormatValue(object? value) =>
        value is null ? $"[gray]<null>[/]" :
        Markup.Escape(
            value.GetType().IsAnonymous
                ? JsonConvert.SerializeObject(value, Formatting.Indented, SerializerSettings)
                : $"{value}"
        );

    readonly Inspection _inspection;
    readonly StackTrace _stackTrace;
    readonly Func<T> _apply;
    readonly ICaptureType _captureType;
    readonly T? _givenTarget;
    readonly bool _initial;

    public Capture(Inspection inspection, StackTrace stackTrace, Func<T> create, ICaptureType captureType)
        : this(inspection, stackTrace, create, captureType, default, initial: true) { }

    public Capture(Inspection inspection, StackTrace stackTrace, Action update, ICaptureType captureType, T target)
        : this(inspection, stackTrace, () => { update(); return target; }, captureType, target, initial: false) { }

    Capture(Inspection inspection, StackTrace stackTrace, Func<T> apply, ICaptureType captureType, T? givenTarget, bool initial)
    {
        _inspection = inspection;
        _stackTrace = stackTrace;
        _apply = apply;
        _captureType = captureType;
        _givenTarget = givenTarget;
        _initial = initial;
    }

    string Property => Markup.Escape(_inspection.Expression.StripNoiseFromExpressionString());

    public T Execute()
    {
        TryEvaluate(_givenTarget, out var previousValue, out var _);
        var target = _apply();
        if (!TryEvaluate(target, out var value, out var type)) { return target; }

        if (_initial)
        {
            Diagnostics.Current.ReportInfo($"[steelblue3]{_captureType.BuildTitle(type)}[/] [gray]{_captureType.Id}[/]", group: _captureType.Id);
        }

        if (!_initial && Equals(value, previousValue)) { return target; }

        var source = _stackTrace.TryFindFeatureSource(out var featureSource)
            ? $"[magenta]{featureSource}[/] {OrderInfo()}"
            : $"[magenta]<unknown>[/] {OrderInfo()}{Environment.NewLine}[gray]{Markup.Escape($"{_stackTrace}")}[/]";
        Diagnostics.Current.ReportInfo($"  [darkgoldenrod]{Property}:[/] {FormatValue(value)} [gray]«[/] {source}", group: _captureType.Id);

        return target;
    }

    bool TryEvaluate(T? target, out object? value, [NotNullWhen(true)] out Type? concreteTypeOfTarget)
    {
        value = null;
        concreteTypeOfTarget = null;

        var targetObject = _captureType.ConvertTarget(target);
        if (targetObject is null || !targetObject.GetType().IsAssignableTo(_inspection.TargetType)) { return false; }

        value = _inspection.Evaluate(targetObject);
        concreteTypeOfTarget = targetObject.GetType();

        return true;
    }

    string OrderInfo() =>
        $"{(_captureType.OrderInfo == null ? string.Empty : $"[gray] Order: {_captureType.OrderInfo}[/]")}";
}