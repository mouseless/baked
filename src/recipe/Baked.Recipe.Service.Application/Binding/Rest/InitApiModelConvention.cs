using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Binding.Rest;

public class InitApiModelConvention : IDomainModelConvention<TypeModelContext>, IDomainModelConvention<MethodModelContext>, IDomainModelConvention<ParameterModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGetSingle<ControllerModelAttribute>(out var controller)) { return; }
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (controller.Initialized) { return; }

        controller.Init(
            id: context.Type.CSharpFriendlyFullName,
            className: context.Type.CSharpFriendlyFullName.Split('.').Skip(1).Join('_'),
            groupName: context.Type.Name,
            actions: members.Methods
                .Having<ActionModelAttribute>()
                .Select(method => method
                    .GetSingle<ActionModelAttribute>()
                    .Init(
                        id: method.Name,
                        returnType: method.DefaultOverload.ReturnType.CSharpFriendlyFullName,
                        returnIsAsync: method.DefaultOverload.ReturnType.IsAssignableTo<Task>(),
                        returnIsVoid: method.DefaultOverload.ReturnType.Is(typeof(void)) || method.DefaultOverload.ReturnType.Is<Task>(),
                        parameters: method.DefaultOverload.Parameters
                            .Having<ParameterModelAttribute>()
                            .Select(param => param
                                .GetSingle<ParameterModelAttribute>()
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
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }
        if (action.Initialized) { return; }

        action.Init(
            id: context.Method.Name,
            returnType: context.Method.DefaultOverload.ReturnType.CSharpFriendlyFullName,
            returnIsAsync: context.Method.DefaultOverload.ReturnType.IsAssignableTo<Task>(),
            returnIsVoid: context.Method.DefaultOverload.ReturnType.Is(typeof(void)) || context.Method.DefaultOverload.ReturnType.Is<Task>(),
            parameters: context.Method.DefaultOverload.Parameters
                .Having<ParameterModelAttribute>()
                .Select(param => param
                    .GetSingle<ParameterModelAttribute>()
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
        if (!context.Parameter.TryGetSingle<ParameterModelAttribute>(out var parameter)) { return; }
        if (parameter.Initialized) { return; }

        parameter.Init(
            id: context.Parameter.Name,
            type: context.Parameter.ParameterType.CSharpFriendlyFullName,
            isOptional: context.Parameter.IsOptional,
            defaultValue: context.Parameter.DefaultValue
        );
    }
}