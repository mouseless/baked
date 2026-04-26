using Baked.Ui;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Baked.Theme;

public class Inspect
{
    public static Inspect? Current { get; private set; }

    public static InspectTrace TraceHere() =>
        new InspectTrace(new StackTrace(fNeedFileInfo: true));

    public static void Component<T>(Expression<Func<T, object?>> evaluate) where T : IComponentSchema =>
        Where(_ => true).Component(evaluate);

    public static void Schema<T>(Expression<Func<T, object?>> evaluate) =>
        Where(_ => true).Schema(evaluate);

    public static ContextPart Where(Func<ComponentContext, bool> where) =>
        new(where);

    public class ContextPart(Func<ComponentContext, bool> where)
    {
        public void Component<T>(Expression<Func<T, object?>> evaluate) where T : IComponentSchema =>
            Current = new(where, typeof(T), c => evaluate.Compile().Invoke((T)c), evaluate.ToString());

        public void Schema<T>(Expression<Func<T, object?>> evaluate) =>
            Current = new(where, typeof(T), c => evaluate.Compile().Invoke((T)c), evaluate.ToString());
    }

    public Func<ComponentContext, bool> Filter { get; }
    public Type SchemaType { get; }
    public Func<object, object?> Evaluate { get; }
    public string Expression { get; }

    Inspect(Func<ComponentContext, bool> filter, Type componentType, Func<object, object?> evaluate, string expression)
    {
        Filter = filter;
        SchemaType = componentType;
        Evaluate = evaluate;
        Expression = expression;
    }
}