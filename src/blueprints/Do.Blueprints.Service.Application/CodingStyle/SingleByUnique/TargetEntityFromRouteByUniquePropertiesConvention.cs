using Do.Business;
using Do.Domain.Model;
using Do.RestApi.Configuration;
using Humanizer;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Do.CodingStyle.SingleByUnique;

public class TargetEntityFromRouteByUniquePropertiesConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    protected DomainModel Domain => _domain;

    protected virtual bool TryGetEntityType(ParameterModelContext context, [NotNullWhen(true)] out TypeModel? entityType, out TypeModel? castTo)
    {
        entityType = context.Parameter.TypeModel;
        castTo = null;

        return true;
    }

    public void Apply(ParameterModelContext context)
    {
        if (context.Parameter.IsInvokeMethodParameter) { return; }
        if (context.Action.MethodModel is null) { return; }
        if (context.Action.MethodModel.Has<InitializerAttribute>()) { return; }

        if (!TryGetEntityType(context, out var entityType, out var castTo)) { return; }
        if (!entityType.TryGetQueryType(_domain, out var queryType)) { return; }
        if (!queryType.TryGetMembers(out var queryMembers)) { return; }

        var singleByUniques = queryMembers.Methods.Having<SingleByUniqueAttribute>();
        if (!singleByUniques.Any()) { return; }

        var uniques = singleByUniques.Select(sbu => sbu.GetSingle<SingleByUniqueAttribute>());
        context.Parameter.Type = "string";
        context.Parameter.Name = $"idOr{uniques.Select(u => u.PropertyName).Join("Or")}";
        context.Action.Route = context.Action.Route.Replace("{id:guid}", $"{{{context.Parameter.Name}}}");

        var findTargetStatements = new StringBuilder();
        findTargetStatements.Append($$"""
            {{context.Parameter.TypeModel.CSharpFriendlyFullName}} __foundTarget = null;
            if(Guid.TryParse({{context.Parameter.Name}}, out var id))
            {
                __foundTarget = {{context.Action.FindTargetStatement}};
            }
        """);

        var queryParameter = context.Action.AddAsService(queryType);
        SingleByUniqueAttribute? uniqueString = null;
        foreach (var singleByUnique in singleByUniques)
        {
            var overload = singleByUnique.Overloads.Single(); // TODO use default overload
            var unique = singleByUnique.GetSingle<SingleByUniqueAttribute>();
            var uniqueParameter = overload.Parameters[unique.PropertyName.Camelize()];
            if (uniqueParameter.ParameterType.IsEnum)
            {
                findTargetStatements.Append($$"""
                    else if(Enum.TryParse<{{uniqueParameter.ParameterType.CSharpFriendlyFullName}}>({{context.Parameter.Name}}, true, out var @{{uniqueParameter.Name}}))
                    {
                        __foundTarget = {{queryParameter.BuildSingleBy($"@{uniqueParameter.Name}", property: unique.PropertyName, fromRoute: true, castTo: castTo)}};
                    }
                """);
            }
            else if (!uniqueParameter.ParameterType.Is<string>() && uniqueParameter.ParameterType.IsAssignableTo(typeof(IParsable<>)))
            {
                findTargetStatements.Append($$"""
                    else if({{uniqueParameter.ParameterType.CSharpFriendlyFullName}}.TryParse((IFormatProvider)null, out var @{{uniqueParameter.Name}}))
                    {
                        __foundTarget = {{queryParameter.BuildSingleBy($"@{uniqueParameter.Name}", property: unique.PropertyName, fromRoute: true, castTo: castTo)}};
                    }
                """);
            }
            else if (uniqueParameter.ParameterType.Is<string>())
            {
                uniqueString = unique;
            }
        }

        if (uniqueString is not null)
        {
            findTargetStatements.Append($$"""
                else
                {
                    __foundTarget = {{queryParameter.BuildSingleBy(context.Parameter.Name, property: uniqueString.PropertyName, fromRoute: true, castTo: castTo)}};
                }
            """);
        }
        else
        {
            findTargetStatements.Append($$"""
                else
                {
                    throw new {{typeof(RouteParameterIsNotValidException).FullName}}("{{context.Parameter.Name}}", {{context.Parameter.Name}});
                }
            """);
        }

        context.Action.PreparationStatements.Add(findTargetStatements.ToString());
        context.Action.FindTargetStatement = "__foundTarget";
    }
}