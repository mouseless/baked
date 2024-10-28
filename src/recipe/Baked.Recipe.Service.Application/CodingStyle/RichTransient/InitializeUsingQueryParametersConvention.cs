using Baked.Business;
using Baked.RestApi.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.RichTransient;

public class InitializeUsingQueryParametersConvention : IApiModelConvention<ActionModelContext>
{
    public void Apply(ActionModelContext context)
    {
        if (!context.Controller.MappedType.TryGetMembers(out var members)) { return; }
        if (!members.Methods.Having<InitializerAttribute>().Any()) { return; }
        if (members.Has<LocatableAttribute>()) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        foreach (var parameter in initializer.DefaultOverload.Parameters)
        {
            context.Action.Parameter[parameter.Name] =
                new(parameter.ParameterType, ParameterModelFrom.Query, parameter.Name, MappedParameter: parameter)
                {
                    IsOptional = parameter.IsOptional,
                    DefaultValue = parameter.DefaultValue,
                    IsInvokeMethodParameter = false
                };
        }
    }
}