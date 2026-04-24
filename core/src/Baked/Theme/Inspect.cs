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

    public static Session Start() =>
        new Session(new StackTrace(fNeedFileInfo: true));

    public class Session(StackTrace _stackTrace)
    {
        static ComponentPath _lastPath;

        public object? Evaluate<T>(T result)
        {
            if (Current is null) { return null; }

            object? target = result is IComponentDescriptor descriptor
                ? descriptor.Schema
                : result;
            if (target?.GetType() != Current._schemaType) { return null; }

            return Current._evaluate(target);
        }

        public T Capture<T>(ComponentContext context, T result,
            object? old = null
        ) => Capture(context, result, out var _, old: old);

        public T Capture<T>(ComponentContext context, T result, out object? value,
            object? old = null
        )
        {
            value = null;
            if (Current is null) { return result; }
            if (!Current._where(context)) { return result; }

            object? target = result is IComponentDescriptor descriptor
                ? descriptor.Schema
                : result;
            if (target?.GetType() != Current._schemaType) { return result; }

            value = Current._evaluate(target);
            if (!Equals(value, old))
            {
                if (_lastPath != context.Path)
                {
                    _lastPath = context.Path;
                    Diagnostics.ReportInfo($"{_gray}{_lastPath}{_reset}");
                }

                var featureLine = _stackTrace.ToString().Split(Environment.NewLine).FirstOrDefault(f => f.Contains("Feature.<>c.<Configure>"));
                if (featureLine is not null)
                {
                    featureLine = featureLine[6..featureLine.IndexOf(".<>")];
                }

                Diagnostics.ReportInfo($"{NullSafe(value)} ⤌ {_gray}{featureLine}{_reset}");
            }

            return result;
        }
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