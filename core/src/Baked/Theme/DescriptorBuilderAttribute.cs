namespace Baked.Theme;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class DescriptorBuilderAttribute<T>
    : Attribute, IComponentContextBasedBuilder<T>, IComponentContextFilter
{
    public Func<ComponentContext, T> Builder { get; set; } = _ => throw DiagnosticCode.InvalidState.Exception($"`Builder` is required to be set for a descriptor, but not set to this instance.");
    public Func<ComponentContext, bool> Filter { get; set; } = cc => true;
    public required Inspect.Session Inspect { get; init; }

    protected T Build(ComponentContext context)
    {
        ComponentPath.AddPath(context.Path);

        var old = context.Inspect;
        context.Inspect = Inspect;

        try
        {
            return Builder(context);
        }
        finally { context.Inspect = old; }
    }

    T IComponentContextBasedBuilder<T>.Build(ComponentContext context) =>
        Build(context);

    bool IComponentContextFilter.AppliesTo(ComponentContext context) =>
        Filter(context);
}