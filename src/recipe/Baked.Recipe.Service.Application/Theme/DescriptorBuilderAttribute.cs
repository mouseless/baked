namespace Baked.Theme;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class DescriptorBuilderAttribute<T> : Attribute, IComponentContextBasedBuilder<T>, IComponentContextFilter
{
    public Func<ComponentContext, T> Builder { get; set; } = _ => throw new ArgumentNullException(nameof(Builder), $"`Builder` was not set for this attribute");
    public Func<ComponentContext, bool> Filter { get; set; } = cc => true;

    T IComponentContextBasedBuilder<T>.Build(ComponentContext context) =>
        Builder(context);

    bool IComponentContextFilter.AppliesTo(ComponentContext context) =>
        Filter(context);
}