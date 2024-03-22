using Do.Domain.Convention;
using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class AttributeAdder(ITypeModelFactory _factory) : IModelConfigurer
{
    internal void Add(IModelWithMetadata model, Attribute attribute)
    {
        model.CustomAttributes.TryAdd(_factory.Create(attribute));
    }
}

public static class AttributeAdderExtensions
{
    public static ModelConventionCollection<T> Add<T>(this ModelConventionCollection<T> source, Attribute add, Func<T, bool> when, int order)
        where T : IModelWithMetadata
    => source.Add((model, adder) => adder.Add(model, add), when, order);

    public static ModelConventionCollection<T> Add<T>(this ModelConventionCollection<T> source, Attribute[] add, Func<T, bool> when, int order)
        where T : IModelWithMetadata
    => source.Add((model, adder) => Array.ForEach(add, a => adder.Add(model, a)), when, order);

    public static ModelConventionCollection<T> Add<T>(this ModelConventionCollection<T> source, Action<T, AttributeAdder> add, Func<T, bool> when, int order)
        where T : IModelWithMetadata
    {
        source.Add(new ModelConvention<T, AttributeAdder>(_apply: add, _appliesTo: when, _order: order));

        return source;
    }
}
