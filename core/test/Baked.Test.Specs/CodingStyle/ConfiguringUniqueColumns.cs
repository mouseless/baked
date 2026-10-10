using Baked.Playground.Orm;

using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked.Test.CodingStyle;

public class ConfiguringUniqueColumns : TestSpec
{
    [Test]
    public void Single_by_query_with_a_single_parameter_causes_corresponding_property_to_be_unique()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var mapping = configuration.GetClassMapping(typeof(Entity));
        var property = configuration.GetClassMapping(typeof(Entity)).PropertyIterator.FirstOrDefault(p => p.Name is nameof(Entity.Unique));
        var column = property?.ColumnIterator.OfType<NHibernate.Mapping.Column>().FirstOrDefault();

        column?.Unique.ShouldBeTrue();
    }
}