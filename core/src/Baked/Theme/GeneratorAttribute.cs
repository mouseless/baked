using Baked.Domain.Inspection;

namespace Baked.Theme;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class GeneratorAttribute<T> : Attribute, IComponentContextBasedGenerator<T>, IComponentContextFilter
{
    public Func<ComponentContext, T> Generator { get; set; } = _ => throw DiagnosticCode.InvalidState.Exception($"`{nameof(Generator)}` is required to be set for a descriptor, but is not set for this instance.");
    public Func<ComponentContext, bool> Filter { get; set; } = cc => true;
    public required Trace Trace { get; init; }

    protected T Generate(ComponentContext context)
    {
        ComponentPath.AddPath(context.Path);
        context.Trace = Trace;

        return Generator(context);
    }

    T IComponentContextBasedGenerator<T>.Generate(ComponentContext context) =>
        Generate(context);

    bool IComponentContextFilter.AppliesTo(ComponentContext context) =>
        Filter(context);
}