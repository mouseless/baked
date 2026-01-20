using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Orm;

public class SingleByIdConvention<T> : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.Is<T>()) { return; }
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGet<ControllerModelAttribute>(out var controller)) { return; }
        if (!metadata.TryGetEntityType(context.Domain, out var entityType)) { return; }

        var queryContextTypeId = context.Domain.Types[typeof(IQueryContext<>)].MakeGenericTypeId(entityType);

        if (!entityType.TryGetIdInfo(out var idInfo)) { return; }

        var singleByActionName = $"SingleBy{idInfo.PropertyName}";
        controller.Action[singleByActionName] = new(singleByActionName,
            routeParts: [context.Type.Name],
            returnType: entityType.CSharpFriendlyFullName,
            returnIsAsync: false,
            returnIsVoid: false,
            parameters:
            [
                new(ParameterModelAttribute.TargetParameterName, context.Domain.Types[queryContextTypeId].CSharpFriendlyFullName, ParameterModelFrom.Services),
                new(idInfo.RouteName, idInfo.Type, ParameterModelFrom.Route) { RoutePosition = 1 },
                new("throwNotFound", context.Domain.Types[typeof(bool)].CSharpFriendlyFullName, ParameterModelFrom.Query) { IsHardCoded = true, LookupRenderer = _ => "true" }
            ]
        )
        {
            Method = HttpMethod.Get,
            FindTargetStatement = ParameterModelAttribute.TargetParameterName
        };
    }
}