using Baked.Architecture;
using Baked.Business;
using Baked.Orm;
using Baked.RestApi.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.CodingStyle.RichEntity;

public class RichEntityCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Index.Type.Add<EntityAttribute>();

            builder.Conventions.SetTypeAttribute(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Constructors.Any(o => o.Parameters.Any(p => p.ParameterType.IsAssignableTo(typeof(IQueryContext<>)))),
                apply: (context, set) =>
                {
                    var query = context.Type;
                    var parameter =
                        query.GetMembers()
                            .Constructors
                            .SelectMany(o => o.Parameters)
                            .First(p => p.ParameterType.IsAssignableTo(typeof(IQueryContext<>)));

                    var entity = parameter.ParameterType.GetGenerics().GenericTypeArguments.First().Model;
                    entity.Apply(t =>
                        set(query, new QueryAttribute(t))
                    );
                    query.Apply(t =>
                        set(entity.GetMetadata(), new EntityAttribute(t))
                    );
                }
            );
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Has<EntityAttribute>(),
                apply: (c, set) =>
                {
                    set(c.Type, new ApiInputAttribute());
                    c.Type.Apply(t =>
                    {
                        set(c.Type, new LocatableAttribute(
                            ServiceType: typeof(ILocator<>).MakeGenericType(t),
                            LocateMethodName: "Locate"
                        )
                        {
                            LocateManyMethodName = "LocateMany"
                        });
                    });
                }
            );
            builder.Conventions.SetMethodAttribute(
                when: c =>
                    c.Type.Has<EntityAttribute>() && c.Method.Has<InitializerAttribute>() &&
                    c.Method.Overloads.Any(o => o.IsPublic && !o.IsStatic && !o.IsSpecialName && o.AllParametersAreApiInput()),
                attribute: c => new ActionModelAttribute(),
                order: 30
            );

            builder.Conventions.Add(new EntityUnderPluralGroupConvention());
            builder.Conventions.Add(new EntityInitializerIsPostResourceConvention());
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
    }
}