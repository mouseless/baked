using Baked.Domain.Configuration;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Baked.Domain.Inspection;

public class Trace(StackTrace? stackTrace)
{
    public static Trace Here() =>
        Inspection.Current is not null
            ? new Trace(new StackTrace(fNeedFileInfo: true))
            : new Trace(null);

    static bool ShouldCapture<TContext>(TContext context, [NotNullWhen(true)] out Inspection? inspection)
        where TContext : DomainModelContext
    {
        inspection = Inspection.Current;

        return
            inspection is not null &&
            (
                !inspection.TryGetFilter<Func<DomainModelContext, bool>>("when", out var when) ||
                when(context)
            );
    }

    public StackTrace StackTrace => stackTrace ?? new();

    public TTarget CaptureAttribute<TModelContext, TTarget>(TModelContext context, Func<TTarget> create,
        string? orderInfo = default
    ) where TModelContext : DomainModelContext
    {
        if (!ShouldCapture(context, out var inspection))
        {
            return create();
        }

        return new Capture<TTarget>(inspection, StackTrace, create, new AttributeCaptureType(context, orderInfo)).Execute();
    }

    public TTarget CaptureAttribute<TModelContext, TTarget>(TModelContext context, TTarget target, Action update,
        string? orderInfo = default
    ) where TModelContext : DomainModelContext
    {
        if (!ShouldCapture(context, out var inspection))
        {
            update();

            return target;
        }

        return new Capture<TTarget>(inspection, StackTrace, update, new AttributeCaptureType(context, orderInfo), target).Execute();
    }
}