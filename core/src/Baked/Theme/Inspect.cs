using System.Diagnostics;
using Baked.Ui;

namespace Baked.Theme;

public class Inspect
{
    static readonly string _gray = "\x1b[90m";
    static readonly string _reset = "\x1b[0m";

    public static Inspect? Current { get; private set; }

    public static ContextPart Where(Func<ComponentContext, bool> where) =>
        new(where);

    public class ContextPart(Func<ComponentContext, bool> where)
    {
        public void Component<T>(Func<T?, object?> evaluate) where T : IComponentSchema =>
            Current = new(where, typeof(T), c => evaluate((T?)c));

        public void Schema<T>(Func<T?, object?> evaluate) =>
            Current = new(where, typeof(T), c => evaluate((T?)c));
    }

    public static object? Capture(ComponentContext context, object? target, StackTrace stackTrace,
        object? old = null,
        bool after = false
    )
    {
        if (Current is null) { return null; }
        if (!Current._where(context)) { return null; }

        if (target is IComponentDescriptor componentDescriptor)
        {
            target = componentDescriptor.Schema;
        }

        if (target?.GetType() != Current._schemaType) { return null; }

        var value = Current._evaluate(target);

        if (after && !Equals(value, old))
        {
            var featureLine = stackTrace.ToString().Split(Environment.NewLine).FirstOrDefault(f => f.Contains("Feature.<>c.<Configure>"));
            if (featureLine is not null)
            {
                featureLine = featureLine[6..featureLine.IndexOf(".<>")];
            }

            Diagnostics.ReportInfo(
                $"{NullSafe(old)} → {NullSafe(value)}" +
                $"{Environment.NewLine}{_gray}\t{context.Path}{_reset}" +
                $"{Environment.NewLine}{_gray}\t{featureLine}{_reset}"
            );
        }

        return value;
    }

    static string NullSafe(object? value) =>
        value is null
            ? $"{_gray}null{_reset}"
            : $"{value}";

    Func<ComponentContext, bool> _where;
    Type _schemaType;
    Func<object?, object?> _evaluate;

    Inspect(Func<ComponentContext, bool> where, Type componentType, Func<object?, object?> evaluate)
    {
        _where = where;
        _schemaType = componentType;
        _evaluate = evaluate;
    }
}