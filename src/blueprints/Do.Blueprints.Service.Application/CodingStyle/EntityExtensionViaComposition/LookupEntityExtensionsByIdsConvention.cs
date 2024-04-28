using Do.Domain.Model;
using Do.Orm;
using Do.RestApi.Configuration;
using Do.RestApi.Model;
using Humanizer;

using ParameterModel = Do.RestApi.Model.ParameterModel;

namespace Do.CodingStyle.EntityExtensionViaComposition;

public class LookupEntityExtensionsByIdsConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        var enumerableParameter = context.Parameter;
        if (!enumerableParameter.IsInvokeMethodParameter) { return; }

        var enumerableType = enumerableParameter.TypeModel;
        if (!enumerableType.IsAssignableTo(typeof(IEnumerable<>))) { return; }
        if (!enumerableType.TryGetGenerics(out var enumerableGenerics)) { return; }

        var entityExtensionType = enumerableType.IsArray ? enumerableGenerics.ElementType : enumerableGenerics.GenericTypeArguments.FirstOrDefault()?.Model;
        if (entityExtensionType is null) { return; }
        if (!entityExtensionType.TryGetMetadata(out var entityExtensionMetadata)) { return; }
        if (!entityExtensionMetadata.TryGetSingle<EntityExtensionAttribute>(out var entityExtensionAttribute)) { return; }

        var entityType = _domain.Types[entityExtensionAttribute.EntityType];
        if (!entityType.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.TryGetSingle<EntityAttribute>(out var entityAttribute)) { return; }

        var queryContextType = _domain.Types[entityAttribute.QueryContextType];
        var queryContextParameter = new ParameterModel(queryContextType, ParameterModelFrom.Services, $"{entityType.Name}Query") { IsInvokeMethodParameter = false };
        context.Action.Parameter[queryContextParameter.Name] = queryContextParameter;

        enumerableParameter.Type = "IEnumerable<Guid>";
        enumerableParameter.Name = $"{enumerableParameter.Name.Singularize()}Ids";
        enumerableParameter.LookupRenderer = parameterExpression => $"{queryContextParameter.Name}.ByIds({parameterExpression}).Select(i => ({entityExtensionType.CSharpFriendlyFullName})i){(enumerableType.IsArray ? ".ToArray()" : ".ToList()")}";
    }
}