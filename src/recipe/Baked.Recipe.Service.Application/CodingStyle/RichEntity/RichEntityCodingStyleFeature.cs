using Baked.Architecture;
using Baked.Business;
using Baked.Orm;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.CodingStyle.RichEntity;

public class RichEntityCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeMetadata(
                apply: (context, add) =>
                {
                    var query = context.Type;
                    var parameter =
                        query.GetMembers()
                            .Constructors
                            .SelectMany(o => o.Parameters)
                            .First(p => p.ParameterType.IsAssignableTo(typeof(IQueryContext<>)));

                    var entity = parameter.ParameterType.GetGenerics().GenericTypeArguments.First().Model;
                    entity.Apply(t =>
                        add(query, new QueryAttribute(t))
                    );
                    query.Apply(t =>
                        add(entity.GetMetadata(), new EntityAttribute(t))
                    );
                },
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Constructors.Any(o => o.Parameters.Any(p => p.ParameterType.IsAssignableTo(typeof(IQueryContext<>))))
            );
            builder.Conventions.AddTypeMetadata(
                apply: (c, add) =>
                {
                    add(c.Type, new ApiInputAttribute());
                    add(c.Type, new LocatableAttribute());
                },
                when: c => c.Type.Has<EntityAttribute>()
            );
            builder.Conventions.AddMethodMetadata(new ApiMethodAttribute(),
                when: c =>
                    c.Type.Has<EntityAttribute>() && c.Method.Has<InitializerAttribute>() &&
                    c.Method.Overloads.Any(o => o.IsPublic && !o.IsStatic && !o.IsSpecialName && o.AllParametersAreApiInput()),
                order: 30
            );
        });

        configurator.ConfigureNHibernateInterceptor(interceptor =>
        {
            interceptor.Instantiator = (ctx, id) =>
            {
                var result = ctx.ApplicationServices.UsingCurrentScope().GetRequiredService(ctx.MetaData.MappedClass);

                ctx.MetaData.SetIdentifier(result, id);

                return result;
            };
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            var domainModel = configurator.Context.GetDomainModel();

            conventions.Add(new EntityUnderPluralGroupConvention());
            conventions.Add(new EntityInitializerIsPostResourceConvention());
            conventions.Add(new FindTargetUsingQueryContextConvention(domainModel), order: 20);
        });
    }
}