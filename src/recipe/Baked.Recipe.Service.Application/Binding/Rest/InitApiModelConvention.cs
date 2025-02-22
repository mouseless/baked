using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Binding.Rest;

public class InitApiModelConvention : IDomainModelConvention<TypeModelContext>, IDomainModelConvention<MethodModelContext>, IDomainModelConvention<ParameterModelContext>
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

    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }

        // to make sure action models without controller model attribute are initiated
        action.Init(
            id: context.Method.Name,
            returnType: context.Method.DefaultOverload.ReturnType.CSharpFriendlyFullName,
            returnIsAsync: context.Method.DefaultOverload.ReturnType.IsAssignableTo<Task>(),
            returnIsVoid: context.Method.DefaultOverload.ReturnType.Is(typeof(void)) || context.Method.DefaultOverload.ReturnType.Is<Task>(),
            parameters: context.Method.DefaultOverload.Parameters
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
        );
    }

    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.TryGetSingle<ParameterModel>(out var parameter)) { return; }

        // to make sure action models without action model attribute are initiated
        parameter.Init(
            id: context.Parameter.Name,
            type: context.Parameter.ParameterType.CSharpFriendlyFullName,
            isOptional: context.Parameter.IsOptional,
            defaultValue: context.Parameter.DefaultValue
        );
    }
}