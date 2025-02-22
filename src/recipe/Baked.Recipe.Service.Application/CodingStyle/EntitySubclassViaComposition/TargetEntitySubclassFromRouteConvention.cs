using Baked.Business;
using Baked.CodingStyle.SingleByUnique;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.EntitySubclassViaComposition;

public class TargetEntitySubclassFromRouteConvention : IDomainModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Method.TryGetSingle<ActionModel>(out var action)) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }
        if (!context.Parameter.TryGetSingle<ParameterModel>(out var parameter)) { return; }
        if (!parameter.IsTarget()) { return; }

        var entitySubclassType = context.Parameter.ParameterType;
        if (!entitySubclassType.TryGetSubclassName(out var subclassName)) { return; }
        if (!entitySubclassType.TryGetEntityTypeFromSubclass(context.Domain, out var entityType)) { return; }
        if (!entityType.TryGetQueryType(context.Domain, out var queryType)) { return; }
        if (!queryType.TryGetMembers(out var queryMembers)) { return; }

        var singleByUniqueMethod = queryMembers.Methods.Having<SingleByUniqueAttribute>().FirstOrDefault();
        if (singleByUniqueMethod is null) { return; }
        if (!singleByUniqueMethod.TryGetSingle<SingleByUniqueAttribute>(out var unique)) { return; }

        var uniqueParameter = singleByUniqueMethod.DefaultOverload.Parameters[unique.PropertyName.Camelize()];
        if (!uniqueParameter.ParameterType.IsEnum && !uniqueParameter.ParameterType.Is<string>()) { return; }

        var valueExpression = uniqueParameter.ParameterType.IsEnum
            ? $"{uniqueParameter.ParameterType.CSharpFriendlyFullName}.{subclassName}"
            : $"\"{subclassName}\"";

        var queryParameter = action.AddAsService(queryType);

        parameter.IsHardCoded = true;
        action.RouteParts = [entityType.Name.Pluralize(), subclassName, action.Name];
        action.FindTargetStatement = queryParameter.BuildSingleBy(valueExpression, property: unique.PropertyName, fromRoute: true, castTo: entitySubclassType);
    }
}