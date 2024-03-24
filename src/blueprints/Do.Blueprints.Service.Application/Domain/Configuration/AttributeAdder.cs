using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class AttributeAdder : IDomainService
{
    static IDomainService IDomainService.New(DomainServiceProvider sp) =>
        new AttributeAdder(sp.Get<ITypeModelFactory>());

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
