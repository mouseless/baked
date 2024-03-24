using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class AttributeAdder : IDomainComponent
{
    static IDomainComponent IDomainComponent.New(BuildDomainContext domainBuilderContext) =>
        new AttributeAdder(domainBuilderContext.Get<ITypeModelFactory>());

    readonly ITypeModelFactory _factory = default!;

    AttributeAdder(ITypeModelFactory factory)
    {
        _factory = factory;
    }

    public void Add<T>(IModelWithMetadata model) =>
        Add(model, typeof(T));

    public void Add(IModelWithMetadata model, Type attributeType)
    {
        if (!attributeType.IsAssignableTo(typeof(Attribute)))
        {
            throw new InvalidOperationException($"{attributeType.Name} is not assignable to 'Attribute'");
        }

        model.CustomAttributes.TryAdd(_factory.Create(attributeType));
    }
}

public static class AttributeAdderExtensions
{
    public static ModelConventionCollection<T> Add<T>(this ModelConventionCollection<T> source, Type add, Func<T, bool> when,
        int? order = default
    )
        where T : IModelWithMetadata
    => source.Add((model, adder) => adder.Add(model, add), when, order);

    public static ModelConventionCollection<T> Add<T>(this ModelConventionCollection<T> source, Type[] add, Func<T, bool> when,
        int? order = default
    )
        where T : IModelWithMetadata
    => source.Add((model, adder) => Array.ForEach(add, a => adder.Add(model, a)), when, order);

    public static ModelConventionCollection<T> Add<T>(this ModelConventionCollection<T> source, Action<T, AttributeAdder> add, Func<T, bool> when,
        int? order = default
    )
        where T : IModelWithMetadata
    {
        source.Add(new ModelConvention<T, AttributeAdder>(_apply: add, _appliesTo: when, _order: order ?? 100));

        return source;
    }
}
