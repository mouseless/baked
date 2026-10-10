using System.Diagnostics.CodeAnalysis;

namespace Baked.Domain.Inspection;

public class Inspection
{
    internal static Inspection? Current
    {
        get;
        set
        {
            if (value is not null && field is not null)
            {
                throw new InvalidOperationException(
                    "There is already an inspection configured:" +
                    $" (TargetType: {field.TargetType.Name}, Expression: {field.Expression})." +
                    " Only one inspection is allowed at a time."
                );
            }

            field = value;
        }
    }

    public static void Clear() =>
        Current = null;

    readonly Dictionary<string, Delegate> _filters = [];

    public Type TargetType { get; }
    public Func<object, object?> Evaluate { get; }
    public string Expression { get; }

    internal Inspection(Type targetType, Func<object, object?> evaluate, string expression)
    {
        TargetType = targetType;
        Evaluate = evaluate;
        Expression = expression;
    }

    public void AddFilter<T>(string name, T filter) where T : Delegate =>
        _filters[name] = filter;

    public bool TryGetFilter<T>(string name, [NotNullWhen(true)] out T? filter)
        where T : Delegate
    {
        filter = default;
        if (!_filters.TryGetValue(name, out var filterObject)) { return false; }

        filter = (T)filterObject;

        return true;
    }
}