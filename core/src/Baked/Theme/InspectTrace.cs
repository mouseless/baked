using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Baked.Theme;

public class InspectTrace(StackTrace _stackTrace)
{
    public T Capture<T>(ComponentContext context, Func<T> create)
    {
        if (!ShouldCapture(context, out var inspect))
        {
            return create();
        }

        return new InspectCapture<T>(inspect, _stackTrace, context, create).Execute();
    }

    public T Capture<T>(ComponentContext context, T target, Action update)
    {
        if (!ShouldCapture(context, out var inspect))
        {
            update();

            return target;
        }

        return new InspectCapture<T>(inspect, _stackTrace, context, update, target).Execute();
    }

    static bool ShouldCapture(ComponentContext context, [NotNullWhen(true)] out Inspect? inspect)
    {
        inspect = Inspect.Current;

        return inspect is not null && inspect.Filter(context);
    }
}