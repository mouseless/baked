using Baked.Business;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;
using Humanizer;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Baked.CodingStyle.SingleByUnique;

public class TargetEntityFromRouteByUniquePropertiesConvention : IDomainModelConvention<MethodModelContext>
{
    protected virtual bool TryGetEntityType(MethodModelContext context, [NotNullWhen(true)] out Domain.Model.TypeModel? entityType, out Domain.Model.TypeModel? castTo)
    {
        entityType = context.Type;
        castTo = null;

        return true;
    }

    public void Apply(MethodModelContext context)
    {
        if (!context.Method.TryGet<ActionModelAttribute>(out var action)) { return; }
        if (!action.Parameter.TryGetValue(ParameterModelAttribute.TargetParameterName, out var parameter)) { return; }
        if (context.Method.Has<InitializerAttribute>()) { return; }

        if (!TryGetEntityType(context, out var entityType, out var castTo)) { return; }
        if (!entityType.TryGetQueryType(context.Domain, out var queryType)) { return; }
        if (!queryType.TryGetMembers(out var queryMembers)) { return; }

        var singleByUniques = queryMembers.Methods.Having<SingleByUniqueAttribute>();
        if (!singleByUniques.Any()) { return; }

        var idAttribute = entityType.GetMembers().FirstProperty<IdAttribute>().Get<IdAttribute>();

        var uniques = singleByUniques.Select(sbu => sbu.Get<SingleByUniqueAttribute>());
        parameter.Type = "string";
        parameter.Name = $"{idAttribute.Key.Kebaberize()}Or{uniques.Select(u => u.PropertyName).Join("Or")}";
        action.RouteParts = action.RouteParts.Replace($"{idAttribute.Key.Kebaberize()}:{idAttribute.Type.Kebaberize()}", $"{{{parameter.Name}}}");

        var findTargetStatements = new StringBuilder();
        findTargetStatements.Append($$"""
            {{context.Type.CSharpFriendlyFullName}} __foundTarget = null;
            if(Guid.TryParse({{parameter.Name}}, out var id))
            {
                __foundTarget = {{action.FindTargetStatement}};
            }
        """);

        var queryParameter = action.AddAsService(queryType);
        SingleByUniqueAttribute? fallback = null;
        foreach (var singleByUnique in singleByUniques)
        {
            var unique = singleByUnique.Get<SingleByUniqueAttribute>();
            var uniqueParameter = singleByUnique.DefaultOverload.Parameters[unique.PropertyName.Camelize()];
            if (uniqueParameter.ParameterType.IsEnum)
            {
                findTargetStatements.Append($$"""
                    else if(Enum.TryParse<{{uniqueParameter.ParameterType.CSharpFriendlyFullName}}>({{parameter.Name}}, true, out var @{{uniqueParameter.Name}}))
                    {
                        __foundTarget = {{queryParameter.BuildSingleBy($"@{uniqueParameter.Name}", unique.PropertyName, fromRoute: true, castTo: castTo)}};
                    }
                """);
            }
            else if (uniqueParameter.ParameterType.Is<string>())
            {
                fallback = unique;
            }
        }

        if (fallback is not null)
        {
            findTargetStatements.Append($$"""
                else
                {
                    __foundTarget = {{queryParameter.BuildSingleBy(parameter.Name, fallback.PropertyName, fromRoute: true, castTo: castTo)}};
                }
            """);
        }
        else
        {
            findTargetStatements.Append($$"""
                else
                {
                    throw new {{nameof(RouteParameterIsNotValidException)}}("{{parameter.Name}}", {{parameter.Name}});
                }
            """);
        }

        action.PreparationStatements.Add(findTargetStatements.ToString());
        action.FindTargetStatement = "__foundTarget";
    }
}