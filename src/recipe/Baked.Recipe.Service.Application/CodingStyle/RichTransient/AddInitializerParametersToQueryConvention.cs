using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.RichTransient;

public class AddInitializerParametersToQueryConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (!context.Type.TryGetMembers(out var members)) { return; }
        if (!members.Methods.Having<InitializerAttribute>().Any()) { return; }
        if (members.Has<LocatableAttribute>()) { return; }
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        foreach (var parameter in initializer.DefaultOverload.Parameters)
        {
            action.Parameter[parameter.Name] =
                new(parameter.Name, parameter.ParameterType.CSharpFriendlyFullName, ParameterModelFrom.Query)
                {
                    IsOptional = parameter.IsOptional,
                    DefaultValue = parameter.DefaultValue,
                    IsInvokeMethodParameter = false
                };
        }
    }
}