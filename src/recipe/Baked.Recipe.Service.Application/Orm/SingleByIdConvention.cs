using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Orm;

public class SingleByIdConvention<T> : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.Is<T>()) { return; }
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGetSingle<ControllerModelAttribute>(out var controller)) { return; }
        if (!metadata.TryGetEntityType(context.Domain, out var entityType)) { return; }

        var queryContextTypeId = context.Domain.Types[typeof(IQueryContext<>)].MakeGenericTypeId(entityType);

        controller.Action["SingleById"] = new("SingleById", HttpMethod.Get, [context.Type.Name], "target",
            returnType: entityType.CSharpFriendlyFullName,
            returnIsAsync: false,
            returnIsVoid: false,
            parameters: [
                new("target", context.Domain.Types[queryContextTypeId].CSharpFriendlyFullName, ParameterModelFrom.Services),
                new("id", context.Domain.Types[typeof(Guid)].CSharpFriendlyFullName, ParameterModelFrom.Route) { RoutePosition = 1 },
                new("throwNotFound", context.Domain.Types[typeof(bool)].CSharpFriendlyFullName, ParameterModelFrom.Query) { IsHardCoded = true, LookupRenderer = _ => "true" }
            ]
        );
    }
}