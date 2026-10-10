using Baked.Domain.Inspection;

namespace Baked.Theme;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class Generator<T> : Attribute, IComponentContextBasedGenerator<T>, IComponentContextFilter
{
    public Func<ComponentContext, T> GeneratorDelegate { get; set; } = _ => throw DiagnosticCode.InvalidState.Exception($"`{nameof(GeneratorDelegate)}` is required to be set for a descriptor, but is not set for this instance.");
    public Func<ComponentContext, bool> FilterDelegate { get; set; } = cc => true;
    public required Trace Trace { get; init; }

    protected T Generate(ComponentContext context)
    {
        ComponentPath.AddPath(context.Path);
        context.Trace = Trace;

        return GeneratorDelegate(context);
    }

    T IComponentContextBasedGenerator<T>.Generate(ComponentContext context) =>
        Generate(context);

    bool IComponentContextFilter.AppliesTo(ComponentContext context) =>
        FilterDelegate(context);
}