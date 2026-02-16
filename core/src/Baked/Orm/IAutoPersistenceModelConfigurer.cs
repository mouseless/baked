using FluentNHibernate.Automapping;

namespace Baked.Orm;

public interface IAutoPersistenceModelConfigurer
{
    void Configure(AutoPersistenceModel model);
}