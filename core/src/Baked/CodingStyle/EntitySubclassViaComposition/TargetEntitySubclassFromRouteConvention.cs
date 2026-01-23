using Baked.Business;
using Baked.Domain.Configuration;
using Humanizer;

namespace Baked.CodingStyle.EntitySubclassViaComposition;

public class TargetEntitySubclassFromRouteConvention : IDomainModelConvention<TypeModelContext>
{
    public void Apply(TypeModelContext context)
    {
        var entitySubclassType = context.Type;
        if (!entitySubclassType.TryGetSubclassName(out var subclassName)) { return; }
        if (!entitySubclassType.TryGetEntityTypeFromSubclass(context.Domain, out var entityType)) { return; }
        if (!entitySubclassType.GetMembers().TryGet<LocatableAttribute>(out var locatable)) { return; }
        if (!entityType.TryGetQueryType(context.Domain, out var queryType)) { return; }
        if (!queryType.TryGetMembers(out var queryMembers)) { return; }

        // TODO requires review
        var singleByUniqueMethod = queryMembers.Methods.FirstOrDefault(m => m.Name.StartsWith("SingleBy"));
        if (singleByUniqueMethod is null) { return; }

        var uniqueParameter = singleByUniqueMethod.DefaultOverload.Parameters.First();
        if (!uniqueParameter.ParameterType.IsEnum && !uniqueParameter.ParameterType.Is<string>()) { return; }

        var valueExpression = uniqueParameter.ParameterType.IsEnum
            ? $"{uniqueParameter.ParameterType.CSharpFriendlyFullName}.{subclassName}"
            : $"\"{subclassName}\"";

        locatable.AddLocatorService = (action) => action.AddAsService(queryType);
        locatable.FindTargetTemplate = (locatableServiceParameter, p) => locatableServiceParameter.BuildSingleBy(valueExpression, property: uniqueParameter.Name.Titleize(), fromRoute: true, castTo: entitySubclassType);
    }
}