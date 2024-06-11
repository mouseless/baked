using NHibernate.Metadata;

namespace Baked.DataAccess;

public record InstantiationContext(IClassMetadata MetaData, IServiceProvider ApplicationServices);