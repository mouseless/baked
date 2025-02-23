using Baked.Domain.Configuration;

namespace Baked.Domain;

public class DomainModelConventionCollection : List<(IDomainModelConvention Convention, int Order)>, IDomainModelConventionCollection;