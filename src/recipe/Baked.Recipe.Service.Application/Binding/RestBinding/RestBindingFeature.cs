using Baked.Architecture;
using Baked.Binding;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.Business.DomainAssemblies;

public class RestBindingFeature : IFeature<BindingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.Add(new InitApiModelConvention(), order: int.MinValue);
        });
    }
}

public class InitApiModelConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGetSingle<ControllerModel>(out var controller)) { return; }

        var actions = new List<ActionModel>();
        if (context.Type.TryGetMembers(out var members))
        {
            foreach (var method in members.Methods.Having<ActionModel>())
            {
                var action = method.GetSingle<ActionModel>();

                action.Init(
                    id: method.Name,
                    defaultReturnType: method.DefaultOverload.ReturnType.CSharpFriendlyFullName,
                    defaultReturnIsAsync: method.DefaultOverload.ReturnType.IsAssignableTo<Task>(),
                    defaultReturnIsVoid: method.DefaultOverload.ReturnType.Is(typeof(void)) || method.DefaultOverload.ReturnType.Is<Task>(),
                    parameters: method.DefaultOverload.Parameters.Having<ParameterModel>().Select(p => p.GetSingle<ParameterModel>())
                );

                actions.Add(action);
            }
        }

        controller.Init(
            id: context.Type.CSharpFriendlyFullName,
            defaultClassName: context.Type.Name,
            defaultGroupName: context.Type.Name,
            actions: actions
        );
    }
}