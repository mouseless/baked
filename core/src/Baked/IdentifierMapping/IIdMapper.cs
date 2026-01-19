using FluentNHibernate.Automapping;

namespace Baked.IdentifierMapping;

public interface IIdMapper
{
    void Configure(AutoPersistenceModel model);
}