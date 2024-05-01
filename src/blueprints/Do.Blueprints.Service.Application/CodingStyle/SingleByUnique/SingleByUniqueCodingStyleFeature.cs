using Do.Architecture;
using Do.Orm;
using Do.Orm.AutoMap;

namespace Do.CodingStyle.SingleByUnique;

public class SingleByUniqueCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Method.Add<SingleByUniqueAttribute>();

            builder.Conventions.AddMethodMetadata(
                apply: (c, add) =>
                {
                    var match = Regexes.StartsWithSingleBy().Match(c.Method.Name);
                    var propertyName = match.Groups["Name"].Value;

                    add(c.Method, new SingleByUniqueAttribute(propertyName));
                },
                when: c =>
                    c.Type.Has<QueryAttribute>() &&
                    Regexes.StartsWithSingleBy().IsMatch(c.Method.Name)
            );
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            var domain = configurator.Context.GetDomainModel();

            conventions.Add(new TargetEntityFromRouteByUniquePropertiesConvention(domain));
            conventions.Add(new UseRouteInSingleByUniqueConvention());
        });
    }
}