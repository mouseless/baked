using Baked.Buildtime.Diagnostics;
using Baked.Domain.Configuration;

namespace Baked.Domain;

public interface IDomainModelConventionCollection : IList<(IDomainModelConvention Convention, int Order)>
{
    Action<DiagnosticsResult>? OnCollectionFinalized { get; }

    void Add(IDomainModelConvention convention, Order order = default);
}