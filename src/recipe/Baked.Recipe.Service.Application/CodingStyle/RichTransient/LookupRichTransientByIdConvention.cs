using Baked.Business;
using Baked.RestApi.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Baked.CodingStyle.RichTransient;

public class LookupRichTransientByIdConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Parameter.MappedParameter is null) { return; }
        if (!context.Parameter.MappedParameter.ParameterType.TryGetMembers(out var members)) { return; }
        if (!members.Has<LocatableAttribute>()) { return; }

        var initializer = members.Methods.Having<InitializerAttribute>().Single();
        if (!initializer.DefaultOverload.Parameters.TryGetValue("id", out var idParameter)) { return; }

        var notNull = context.Parameter.MappedParameter.Has<NotNullAttribute>();
        var factoryParameter = context.Action.AddFactoryAsService(context.Parameter.MappedParameter.ParameterType);

        context.Parameter.Name = $"{context.Parameter.Name}Id";
        context.Parameter.Type = $"{idParameter.ParameterType.CSharpFriendlyFullName}";
        context.Parameter.LookupRenderer = p => factoryParameter.BuildInitializerById(p,
            nullable: !notNull
        );
    }
}