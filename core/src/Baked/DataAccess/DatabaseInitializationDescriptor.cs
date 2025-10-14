using NHibernate;

namespace Baked.DataAccess;

public record DatabaseInitializationDescriptor(Action<ISessionFactory> Initializer);