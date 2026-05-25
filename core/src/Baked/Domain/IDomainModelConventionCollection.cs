using Baked.Domain.Configuration;

namespace Baked.Domain;

public interface IDomainModelConventionCollection : IList<(IDomainModelConvention Convention, Order Order)>
{
    void Add(IDomainModelConvention convention, Func<OrderBuilder, Order>? order = default);
}
