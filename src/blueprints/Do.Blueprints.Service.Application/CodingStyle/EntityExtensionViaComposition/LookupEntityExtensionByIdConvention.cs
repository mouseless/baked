using Do.Business;
using Do.Domain.Model;
using Do.Orm;
using Do.RestApi.Configuration;
using Do.RestApi.Model;
using Humanizer;

using ParameterModel = Do.RestApi.Model.ParameterModel;

namespace Do.CodingStyle.EntityExtensionViaComposition;

public class LookupEntityExtensionByIdConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (context.Action.MethodModel?.Has<InitializerAttribute>() == true) { return; }

        var entityParameter = context.Parameter;
        if (!entityParameter.IsInvokeMethodParameter) { return; }

        var entityExtensionType = entityParameter.TypeModel;
        if (!entityExtensionType.TryGetMetadata(out var entityExtensionMetadata)) { return; }
        if (!entityExtensionMetadata.TryGetSingle<EntityExtensionAttribute>(out var entityExtensionAttribute)) { return; }

        var entityType = _domain.Types[entityExtensionAttribute.EntityType];
        if (!entityType.TryGetMetadata(out var entityMetadata)) { return; }
        if (!entityMetadata.TryGetSingle<EntityAttribute>(out var entityAttribute)) { return; }

        var queryContextType = _domain.Types[entityAttribute.QueryContextType];
        var queryContextParameter = new ParameterModel(queryContextType, ParameterModelFrom.Services, $"{entityType.Name.Camelize()}Query") { IsInvokeMethodParameter = false };
        context.Action.Parameter[queryContextParameter.Name] = queryContextParameter;

        entityParameter.Type = nameof(Guid);
        entityParameter.Name += "Id";
        entityParameter.LookupRenderer = parameterExpression => $"({entityExtensionType.CSharpFriendlyFullName}){queryContextParameter.Name}.SingleById({parameterExpression})";
    }
}