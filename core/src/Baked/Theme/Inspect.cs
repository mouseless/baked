using Baked.Domain.Configuration;
using Baked.Ui;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Baked.Theme;

public class Inspect
{
    public static Inspect? Current { get; private set; }

    public static InspectTrace TraceHere() =>
        new InspectTrace(new StackTrace(fNeedFileInfo: true));

    public static void Attribute<T>(
        Expression<Func<T, object?>>? evaluate = default
    ) where T : Attribute
    {
        evaluate ??= x => x;

        Current = new(_ => true, typeof(T), c => evaluate.Compile().Invoke((T)c), evaluate.ToString());
    }

    public static void Component<T>(
        Expression<Func<T, object?>>? evaluate = default
    ) where T : IComponentSchema =>
        Where(_ => true).Component(evaluate);

    public static void Schema<T>(
        Expression<Func<T, object?>>? evaluate = default
    ) => Where(_ => true).Schema(evaluate);

    public static ContextPart Where(Func<ComponentContext, bool> where) =>
        new(where);

    public class ContextPart(Func<ComponentContext, bool> where)
    {
        public void Component<T>(
            Expression<Func<T, object?>>? evaluate = default
        ) where T : IComponentSchema
        {
            evaluate ??= x => x;

            Current = new(where, typeof(T), c => evaluate.Compile().Invoke((T)c), evaluate.ToString());
        }

        public void Schema<T>(
            Expression<Func<T, object?>>? evaluate = default
        )
        {
            evaluate ??= x => x;

            Current = new(where, typeof(T), c => evaluate.Compile().Invoke((T)c), evaluate.ToString());
        }
    }

    public Func<DomainModelContext, bool> Filter { get; }
    public Func<ComponentContext, bool> ComponentFilter { get; }
    public Type TargetType { get; }
    public Func<object, object?> Evaluate { get; }
    public string Expression { get; }

    Inspect(Func<ComponentContext, bool> componentFilter, Type targetType, Func<object, object?> evaluate, string expression)
    {
        Filter = _ => true;
        ComponentFilter = componentFilter;
        TargetType = targetType;
        Evaluate = evaluate;
        Expression = expression;
    }
}