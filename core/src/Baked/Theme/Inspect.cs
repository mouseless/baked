using Baked.Ui;
using System.Diagnostics;

namespace Baked.Theme;

public class Inspect
{
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

    public static Inspection Start() =>
        new Inspection(new StackTrace(fNeedFileInfo: true));

    public Func<ComponentContext, bool> Filter { get; }
    public Type SchemaType { get; }
    public Func<object?, object?> Evaluate { get; }

    Inspect(Func<ComponentContext, bool> filter, Type componentType, Func<object?, object?> evaluate)
    {
        Filter = filter;
        SchemaType = componentType;
        Evaluate = evaluate;
    }
}