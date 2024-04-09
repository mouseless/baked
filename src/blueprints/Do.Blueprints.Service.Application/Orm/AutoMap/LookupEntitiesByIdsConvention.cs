using Do.Domain.Model;
using Do.RestApi.Configuration;
using Do.RestApi.Model;
using Humanizer;

using ParameterModel = Do.RestApi.Model.ParameterModel;

namespace Do.Orm.AutoMap;

public class LookupEntitiesByIdsConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        var enumerableParameter = context.Parameter;
        if (!enumerableParameter.IsInvokeMethodParameter) { return; }

        var enumerableType = enumerableParameter.TypeModel;
        if (!enumerableType.IsAssignableTo(typeof(IEnumerable<>))) { return; }
        if (!enumerableType.TryGetGenerics(out var enumerableGenerics)) { return; }

        var entityType = enumerableType.IsArray ? enumerableGenerics.ElementType : enumerableGenerics.GenericTypeArguments.FirstOrDefault()?.Model;
        if (entityType is null) { return; }
        if (!entityType.TryGetMetadata(out var metadata) || !metadata.TryGetSingle<EntityAttribute>(out var entityAttribute)) { return; }

        var queryContextType = _domain.Types[entityAttribute.QueryContextType];
        var queryContextParameter = new ParameterModel(queryContextType, ParameterModelFrom.Services, $"{entityType.Name}Query") { IsInvokeMethodParameter = false };
        context.Action.Parameter[queryContextParameter.Name] = queryContextParameter;

        enumerableParameter.Type = "IEnumerable<Guid>";
        enumerableParameter.Name = $"{enumerableParameter.Name.Singularize()}Ids";
        enumerableParameter.LookupRenderer = parameterExpression => $"{queryContextParameter.Name}.ByIds({parameterExpression}){(enumerableType.IsArray ? ".ToArray()" : string.Empty)}";
    }
}