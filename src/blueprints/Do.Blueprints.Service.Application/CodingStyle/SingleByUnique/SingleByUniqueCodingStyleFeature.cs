using Do.Architecture;
using Do.Orm;
using Do.Orm.AutoMap;
using Humanizer;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

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
                    var propertyType = c.Method.Overloads[0].Parameters[propertyName.Camelize()].ParameterType; // TODO use default overload
                    if (propertyType.Is<string>() || propertyType.IsEnum)
                    {
                        propertyType.Apply(t =>
                        {
                            add(c.Method, new SingleByUniqueAttribute(propertyName, t));
                        });
                    }
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
            conventions.Add(new MarkActionAsSingleByUniqueConvention());
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<MatcherPolicy, UniquePropertyMatcherPolicy>();
        });

        configurator.ConfigureSwaggerGenOptions(swaggerGen =>
        {
            swaggerGen.DocumentFilter<UnifyUniquePropertiesInRouteDocumentFilter>();
        });
    }
}