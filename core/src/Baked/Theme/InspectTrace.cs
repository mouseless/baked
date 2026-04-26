using Baked.Domain.Configuration;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Baked.Theme;

public class InspectTrace(StackTrace _stackTrace)
{
    public T Capture<T>(DomainModelContext context, Func<T> create)
    {
        if (!ShouldCapture(context, out var inspect))
        {
            return create();
        }

        return
            new InspectCapture<T>(inspect, _stackTrace, create,
                reportCreate: type => ReportCreate(type, context)
            ).Execute();
    }

    public T Capture<T>(DomainModelContext context, T target, Action update)
    {
        if (!ShouldCapture(context, out var inspect))
        {
            update();

            return target;
        }

        return new InspectCapture<T>(inspect, _stackTrace, update, target).Execute();
    }

    static bool ShouldCapture(DomainModelContext context, [NotNullWhen(true)] out Inspect? inspect)
    {
        inspect = Inspect.Current;

        return inspect is not null && inspect.Filter(context);
    }

    static void ReportCreate(Type type, DomainModelContext context) =>
        Diagnostics.ReportInfo($"[lightskyblue3_1][[{type.Name.Replace("Attribute", string.Empty)}]][/] [gray]{context.Identifier}[/]");

    public T Capture<T>(ComponentContext context, Func<T> create)
    {
        if (!ShouldCapture(context, out var inspect))
        {
            return create();
        }

        return
            new InspectCapture<T>(inspect, _stackTrace, create,
                reportCreate: type => ReportCreate(type, context)
            ).Execute();
    }

    public T Capture<T>(ComponentContext context, T target, Action update)
    {
        if (!ShouldCapture(context, out var inspect))
        {
            update();

            return target;
        }

        return new InspectCapture<T>(inspect, _stackTrace, update, target).Execute();
    }

    static bool ShouldCapture(ComponentContext context, [NotNullWhen(true)] out Inspect? inspect)
    {
        inspect = Inspect.Current;

        return inspect is not null && inspect.ComponentFilter(context);
    }

    static void ReportCreate(Type type, ComponentContext context) =>
        Diagnostics.ReportInfo($"[lightskyblue3_1]<{type.GetName(includeDeclaringTypes: true)}>[/] [gray]{context.Path}[/]");
}