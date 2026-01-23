using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Orm;

namespace Baked.CodingStyle.RichEntity;

public class LocateUsingQueryContextConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }
        if (!metadata.TryGet<LocatableAttribute>(out var locatable)) { return; }

        var entityType = context.Type;
        if (!entityType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }
        if (!entityType.TryGetIdInfo(out var idInfo)) { return; }

        locatable.AddLocatorService = (action) => action.AddQueryContextAsService(queryContextType);
        locatable.FindTargetTemplate = (locatorServiceParameter, parameter) => locatorServiceParameter.BuildSingleBy(parameter.Name, "Id", fromRoute: true);
        locatable.LookupParameterTemplate = (locatorServiceParameter, p, notNull) => locatorServiceParameter.BuildSingleBy(p, idInfo.PropertyName,
            notNullValueExpression: $"({idInfo.Type}){p}",
            nullable: !notNull
        );
        locatable.LookupListParameterTemplate = (locatorServiceParameter, p, isArray) => locatorServiceParameter.BuildByIds(p,
           isArray: isArray
        );
    }
}