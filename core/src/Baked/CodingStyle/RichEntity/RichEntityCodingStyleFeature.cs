using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Model;
using Baked.Orm;
using Baked.RestApi.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.RichEntity;

public class RichEntityCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeAttribute(
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    TryGetEntityContextParameter(members, out var entityContextParameter) &&
                    entityContextParameter.ParameterType.TryGetGenerics(out var entityContextGenerics) &&
                    entityContextGenerics.GenericTypeArguments.First().Model == c.Type,
                attribute: () => new EntityAttribute()
            );
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Has<EntityAttribute>(),
                apply: (c, set) =>
                {
                    set(c.Type, new ApiInputAttribute());
                    set(c.Type, new LocatableAttribute());
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

    bool TryGetEntityContextParameter(TypeModelMembers type, [NotNullWhen(true)] out ParameterModel? parameter)
    {
        parameter = type.Constructors
            .Select(o => o.Parameters.SingleOrDefault(p => p.ParameterType.IsAssignableTo(typeof(IEntityContext<>))))
            .FirstOrDefault(p => p is not null);

        return parameter is not null;
    }
}