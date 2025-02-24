using Baked.Domain.Configuration;

namespace Baked.Domain;

public interface IDomainModelConventionCollection : IList<(IDomainModelConvention Convention, int Order)>;