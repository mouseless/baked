using Baked.Business;
using Baked.RestApi.Configuration;

namespace Baked.CodingStyle.RichTransient;

public class LookUpTransientByIdConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }
        if (!context.Parameter.IsInvokeMethodParameter) { return; }
        if (context.Parameter.MappedParameter is null) { return; }
        if (!context.Parameter.MappedParameter.ParameterType.TryGetMembers(out var members)) { return; }
        if (!members.Has<LocatableAttribute>()) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        if (!initializer.DefaultOverload.Parameters.TryGetValue("id", out var idParameter)) { return; }

        var factoryParameter = context.Action.AddFactoryAsService(context.Parameter.MappedParameter.ParameterType);

        context.Parameter.Name = $"{context.Parameter.Name}Id";
        context.Parameter.Type = $"{idParameter.ParameterType.CSharpFriendlyFullName}";
        context.Parameter.LookupRenderer = p => factoryParameter.BuildInitializerById(p);
    }
}