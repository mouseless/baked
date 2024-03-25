using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class AttributeAdder : IDomainService
{
    static IDomainService IDomainService.New(DomainServiceProvider sp) =>
        new AttributeAdder();

    public void Add(IModel model, Attribute attribute)
    {
        model.CustomAttributes.Add(attribute);
    }
}
