using Baked.Domain.Configuration;

namespace Baked.Domain;

public class DomainModelConventionCollection : List<(IDomainModelConvention Convention, Order Order)>, IDomainModelConventionCollection
{
    public void Add(IDomainModelConvention convention, Func<OrderBuilder, Order>? order = default)
    {
        throw new NotImplementedException();
    }
}