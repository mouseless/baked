using FluentNHibernate.Automapping;

namespace Baked.CodingStyle.Id;

public interface IIdMapper
{
    void Configure(AutoPersistenceModel model);
}