namespace Baked.Theme;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class DescriptorBuilderAttribute<T>
    : Attribute, IComponentContextBasedBuilder<T>, IComponentContextFilter
{
    public Func<ComponentContext, T> Builder { get; set; } = _ => throw DiagnosticCode.InvalidState.Exception($"`Builder` is required to be set for a descriptor, but not set to this instance.");
    public Func<ComponentContext, bool> Filter { get; set; } = cc => true;
    public required InspectTrace Trace { get; init; }

    protected T Build(ComponentContext context)
    {
        ComponentPath.AddPath(context.Path);
        context.Trace = Trace;

        return Builder(context);
    }

    T IComponentContextBasedBuilder<T>.Build(ComponentContext context) =>
        Build(context);

    bool IComponentContextFilter.AppliesTo(ComponentContext context) =>
        Filter(context);
}