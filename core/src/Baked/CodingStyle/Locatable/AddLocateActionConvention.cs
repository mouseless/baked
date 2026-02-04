using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.Locatable;

public class AddLocateActionConvention<T> : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.Is<T>()) { return; }
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGet<ControllerModelAttribute>(out var controller)) { return; }
        if (!metadata.TryGet<LocatableAttribute>(out var locatable)) { return; }
        if (!context.Type.TryGetIdInfo(out var idInfo)) { return; }

        context.Type.Apply(t =>
        {
            controller.Action["Locate"] = new("Locate",
                routeParts: [context.Type.Name.Pluralize()],
                returnType: context.Type.CSharpFriendlyFullName,
                returnIsAsync: locatable.IsAsync,
                returnIsVoid: false,
                parameters:
                [
                    new(ParameterModelAttribute.TargetParameterName, locatable.ServiceType.GetCSharpFriendlyFullName(), ParameterModelFrom.Services),
                    new(idInfo.RouteName, idInfo.Type, ParameterModelFrom.Route) { RoutePosition = 1 },
                    new("throwNotFound", context.Domain.Types[typeof(bool)].CSharpFriendlyFullName, ParameterModelFrom.Query) { IsHardCoded = true, LookupRenderer = _ => "true" }
                ]
            )
            {
                Method = HttpMethod.Get,
                FindTargetStatement = ParameterModelAttribute.TargetParameterName
            };
        });
    }
}