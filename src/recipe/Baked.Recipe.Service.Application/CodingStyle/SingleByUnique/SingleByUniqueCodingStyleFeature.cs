using Baked.Architecture;
using Baked.Orm;
using Baked.Orm.AutoMap;
using Humanizer;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.CodingStyle.SingleByUnique;

public class SingleByUniqueCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Method.Add<SingleByUniqueAttribute>();

            builder.Conventions.SetMethodMetadata(
                apply: (c, set) =>
                {
                    var match = Regexes.StartsWithSingleBy.Match(c.Method.Name);
                    var propertyName = match.Groups["Name"].Value;
                    var propertyType = c.Method.DefaultOverload.Parameters[propertyName.Camelize()].ParameterType;
                    if (propertyType.Is<string>() || propertyType.IsEnum)
                    {
                        propertyType.Apply(t =>
                        {
                            set(c.Method, new SingleByUniqueAttribute(propertyName, t));
                        });
                    }
                },
                when: c =>
                    c.Type.Has<QueryAttribute>() &&
                    Regexes.StartsWithSingleBy.IsMatch(c.Method.Name)
            );

            builder.Conventions.Add(new UseRouteInSingleByUniqueConvention());
            builder.Conventions.Add(new MarkActionAsSingleByUniqueConvention());
            builder.Conventions.Add(new TargetEntityFromRouteByUniquePropertiesConvention(), order: 30);
        });

        configurator.ConfigureApiModel(api =>
        {
            api.Usings.Add("Baked.CodingStyle.SingleByUnique");
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