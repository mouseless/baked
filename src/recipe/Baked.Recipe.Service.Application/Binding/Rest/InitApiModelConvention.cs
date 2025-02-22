using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Binding.Rest;

public class InitApiModelConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGetSingle<ControllerModel>(out var controller)) { return; }
        if (!context.Type.TryGetMembers(out var members)) { return; }

        controller.Init(
            id: context.Type.CSharpFriendlyFullName,
            className: context.Type.Name,
            actions: members.Methods
                .Having<ActionModel>()
                .Select(method => method
                    .GetSingle<ActionModel>()
                    .Init(
                        id: method.Name,
                        returnType: method.DefaultOverload.ReturnType.CSharpFriendlyFullName,
                        returnIsAsync: method.DefaultOverload.ReturnType.IsAssignableTo<Task>(),
                        returnIsVoid: method.DefaultOverload.ReturnType.Is(typeof(void)) || method.DefaultOverload.ReturnType.Is<Task>(),
                        parameters: method.DefaultOverload.Parameters
                            .Having<ParameterModel>()
                            .Select(param => param
                                .GetSingle<ParameterModel>()
                                .Init(
                                    id: param.Name,
                                    type: param.ParameterType.CSharpFriendlyFullName,
                                    isOptional: param.IsOptional,
                                    defaultValue: param.DefaultValue
                                )
                            )
                    )
                )
        );
    }
}