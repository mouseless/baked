using Do.Business;
using Do.Domain.Model;
using Do.Orm;
using Do.RestApi.Configuration;
using Do.RestApi.Model;
using Humanizer;

using ParameterModel = Do.RestApi.Model.ParameterModel;

namespace Do.CodingStyle.EntityExtensionViaComposition;

public class TargetEntityExtensionFromRouteConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Action.MethodModel?.Has<InitializerAttribute>() == true) { return; }

        var target = context.Parameter;
        if (target.IsInvokeMethodParameter) { return; }

        var entityExtensionType = target.TypeModel;
        if (!entityExtensionType.TryGetMetadata(out var entityExtensionMetadata)) { return; }
        if (!entityExtensionMetadata.TryGetSingle<EntityExtensionAttribute>(out var entityExtensionAttribute)) { return; }

        var entityType = _domain.Types[entityExtensionAttribute.EntityType];
        if (!entityType.TryGetMetadata(out var entityMetadata)) { return; }
        if (!entityMetadata.TryGetSingle<EntityAttribute>(out var entityAttribute)) { return; }

        var queryContextType = _domain.Types[entityAttribute.QueryContextType];
        var queryContextParameter = new ParameterModel(queryContextType, ParameterModelFrom.Services, $"{entityType.Name.Camelize()}Query") { IsInvokeMethodParameter = false };
        context.Action.Parameter[queryContextParameter.Name] = queryContextParameter;

        target.Type = nameof(Guid);
        target.Name = $"{entityType.Name.Camelize()}Id";
        target.From = ParameterModelFrom.Route;

        context.Action.Route = $"{entityType.Name.Pluralize()}/{{{target.Name}:guid}}/{context.Action.Name}";
        context.Action.FindTargetStatement = $"({entityExtensionType.CSharpFriendlyFullName}){queryContextParameter.Name}.SingleById({target.Name}, throwNotFound: true)";
    }
}