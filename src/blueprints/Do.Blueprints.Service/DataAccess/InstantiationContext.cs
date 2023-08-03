using NHibernate.Metadata;

namespace Do.DataAccess;

public record InstantiationContext(IClassMetadata MetaData, IServiceProvider ApplicationServices);
