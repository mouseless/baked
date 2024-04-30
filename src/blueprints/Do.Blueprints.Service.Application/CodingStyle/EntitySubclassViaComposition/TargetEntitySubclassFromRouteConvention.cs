using Do.Business;
using Do.CodingStyle.SingleByUnique;
using Do.Domain.Model;
using Do.RestApi.Configuration;
using Humanizer;

namespace Do.CodingStyle.EntitySubclassViaComposition;

public class TargetEntitySubclassFromRouteConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Action.MethodModel?.Has<InitializerAttribute>() == true) { return; }
        if (context.Parameter.IsInvokeMethodParameter) { return; }

        var entitySubclassType = context.Parameter.TypeModel;
        if (!entitySubclassType.TryGetSubclassName(out var subclassName)) { return; }
        if (!entitySubclassType.TryGetEntityTypeFromSubclass(_domain, out var entityType)) { return; }
        if (!entityType.TryGetQueryType(_domain, out var queryType)) { return; }
        if (!queryType.TryGetMembers(out var queryMembers)) { return; }

        var singleByUniqueMethod = queryMembers.Methods.Having<SingleByUniqueAttribute>().FirstOrDefault();
        if (singleByUniqueMethod is null) { return; }
        if (!singleByUniqueMethod.TryGetSingle<SingleByUniqueAttribute>(out var unique)) { return; }
        if (singleByUniqueMethod.Overloads.Count != 1) { return; } // TODO will use default overload

        var uniqueParameter = singleByUniqueMethod.Overloads[0].Parameters[unique.PropertyName.Camelize()];
        if (!uniqueParameter.ParameterType.IsEnum && !uniqueParameter.ParameterType.Is<string>()) { return; }

        var valueExpression = uniqueParameter.ParameterType.IsEnum
            ? $"{uniqueParameter.ParameterType.CSharpFriendlyFullName}.{subclassName}"
            : $"\"{subclassName}\"";

        var queryParameter = context.Action.AddAsService(queryType);

        context.Parameter.IsHardCoded = true;
        context.Action.Route = $"{entityType.Name.Pluralize()}/{subclassName}/{context.Action.Name}";
        context.Action.FindTargetStatement = queryParameter.BuildSingleBy(valueExpression, property: unique.PropertyName, fromRoute: true, castTo: entitySubclassType);
    }
}