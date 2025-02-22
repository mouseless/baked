using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Business.DomainAssemblies;

public class InitApiModelConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGetSingle<ControllerModel>(out var controller)) { return; }

        controller.Init(
            id: context.Type.CSharpFriendlyFullName,
            defaultClassName: context.Type.Name,
            defaultGroupName: context.Type.Name,
            actions: context.Type.TryGetMembers(out var members)
                ? members.Methods
                    .Having<ActionModel>()
                    .Select(method => method
                        .GetSingle<ActionModel>()
                        .Init(
                            id: method.Name,
                            defaultReturnType: method.DefaultOverload.ReturnType.CSharpFriendlyFullName,
                            defaultReturnIsAsync: method.DefaultOverload.ReturnType.IsAssignableTo<Task>(),
                            defaultReturnIsVoid: method.DefaultOverload.ReturnType.Is(typeof(void)) || method.DefaultOverload.ReturnType.Is<Task>(),
                            parameters: method.DefaultOverload.Parameters
                                .Having<ParameterModel>()
                                .Select(param => param
                                    .GetSingle<ParameterModel>()
                                    .Init(param.Name, param.ParameterType.CSharpFriendlyFullName)
                                )
                        )
                    )
                : []
        );
    }
}