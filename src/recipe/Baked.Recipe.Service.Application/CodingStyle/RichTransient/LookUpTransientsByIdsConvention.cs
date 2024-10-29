using Baked.Business;
using Baked.RestApi.Configuration;
using Humanizer;

namespace Baked.CodingStyle.RichTransient;

public class LookUpTransientsByIdsConvention : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Action.MappedMethod is null) { return; }
        if (context.Action.MappedMethod.Has<InitializerAttribute>()) { return; }
        if (!context.Parameter.IsInvokeMethodParameter) { return; }
        if (context.Parameter.MappedParameter is null) { return; }
        if (!context.Parameter.MappedParameter.ParameterType.TryGetElementType(out var elementType)) { return; }
        if (!elementType.GetMetadata().Has<LocatableAttribute>()) { return; }

        var initializer = elementType.GetMembers().Methods.Having<InitializerAttribute>().Single();
        if (!initializer.DefaultOverload.Parameters.TryGetValue("id", out var idParameter)) { return; }

        var factoryParameter = context.Action.AddFactoryAsService(elementType);

        context.Parameter.Type = $"IEnumerable<{idParameter.ParameterType.CSharpFriendlyFullName}>";
        context.Parameter.Name = $"{context.Parameter.Name.Singularize()}Ids";
        context.Parameter.LookupRenderer = p => factoryParameter.BuildInitializerByIds(p, isArray: context.Parameter.TypeModel.IsArray);
    }
}