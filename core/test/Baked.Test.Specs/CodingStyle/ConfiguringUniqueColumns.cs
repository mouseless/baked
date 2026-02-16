using Baked.Playground.CodingStyle.EntitySubclass;
using Baked.Playground.Orm;

using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked.Test.CodingStyle;

public class ConfiguringUniqueColumns : TestSpec
{
    [Test]
    public void Any_by_query_with_a_single_parameter_causes_corresponding_property_to_be_unique()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var mapping = configuration.GetClassMapping(typeof(Entity));
        var property = configuration.GetClassMapping(typeof(Entity)).PropertyIterator.FirstOrDefault(p => p.Name == nameof(Entity.Enum));
        var column = property?.ColumnIterator.OfType<NHibernate.Mapping.Column>().FirstOrDefault();

        column?.Unique.ShouldBeTrue();
    }

    [Test]
    public void Single_by_query_with_a_single_parameter_causes_corresponding_property_to_be_unique()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var mapping = configuration.GetClassMapping(typeof(TypedEntity));
        var property = configuration.GetClassMapping(typeof(TypedEntity)).PropertyIterator.FirstOrDefault(p => p.Name == nameof(TypedEntity.Type));
        var column = property?.ColumnIterator.OfType<NHibernate.Mapping.Column>().FirstOrDefault();

        column?.Unique.ShouldBeTrue();
    }
}