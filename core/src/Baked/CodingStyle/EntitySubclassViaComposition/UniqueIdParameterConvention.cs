using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.EntitySubclassViaComposition;

public class UniqueIdParameterConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        var entitySubclassType = context.Type;
        if (!entitySubclassType.TryGetSubclassName(out var subclassName)) { return; }
        if (!entitySubclassType.TryGetEntityTypeFromSubclass(context.Domain, out var entityType)) { return; }
        if (!entitySubclassType.GetMembers().Has<LocatableAttribute>()) { return; }
        if (!entityType.TryGetQueryType(context.Domain, out var queryType)) { return; }
        if (!queryType.TryGetMembers(out var queryMembers)) { return; }
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }

        // TODO requires review
        var singleByUniqueMethod = queryMembers.Methods.FirstOrDefault(m => m.Name.StartsWith("SingleBy"));
        if (singleByUniqueMethod is null) { return; }

        var uniqueParameter = singleByUniqueMethod.DefaultOverload.Parameters.First();
        if (!uniqueParameter.ParameterType.IsEnum && !uniqueParameter.ParameterType.Is<string>()) { return; }

        var valueExpression = uniqueParameter.ParameterType.IsEnum
            ? $"{uniqueParameter.ParameterType.CSharpFriendlyFullName}.{subclassName}"
            : $"\"{subclassName}\"";

        var id = action.Parameter[ParameterModelAttribute.TargetParameterName];
        id.From = ParameterModelFrom.Inline;
        id.IsHardCoded = true;
        id.InternalName = uniqueParameter.Name.Camelize();
        id.Name = uniqueParameter.Name.Camelize();
        id.Type = uniqueParameter.ParameterType.CSharpFriendlyFullName;
        id.LookupRenderer = _ => valueExpression;
    }
}