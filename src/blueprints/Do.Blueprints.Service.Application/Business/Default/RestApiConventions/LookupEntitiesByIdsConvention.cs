using Do.Domain.Model;
using Do.Orm;
using Do.RestApi.Configuration;
using Do.RestApi.Model;
using Humanizer;

using ParameterModel = Do.RestApi.Model.ParameterModel;

namespace Do.Business.Default.RestApiConventions;

public class LookupEntitiesByIdsConvention(DomainModel _domain)
    : IApiModelConvention<ParameterModelContext>
{
    public void Apply(ParameterModelContext context)
    {
        if (!context.Parameter.IsInvokeMethodParameter) { return; }

        var enumerableType = context.Parameter.TypeModel;
        if (!enumerableType.IsAssignableTo(typeof(IEnumerable<>))) { return; }
        if (!enumerableType.TryGetGenerics(out var enumerableGenerics)) { return; }

        var entityType = enumerableType.IsArray ? enumerableGenerics.ElementType : enumerableGenerics.GenericTypeArguments.FirstOrDefault()?.Model;
        if (entityType is null) { return; }
        if (!entityType.TryGetMetadata(out var metadata) || !metadata.TryGetSingle<EntityAttribute>(out var entityAttribute)) { return; }

        var queryContextType = _domain.Types[entityAttribute.QueryContextType];
        var queryContextParameter = new ParameterModel(queryContextType, ParameterModelFrom.Services, $"{entityType.Name}Query") { IsInvokeMethodParameter = false };
        context.Action.Parameter[queryContextParameter.Name] = queryContextParameter;

        context.Parameter.Type = "IEnumerable<Guid>";
        context.Parameter.Name = $"{context.Parameter.Name.Singularize()}Ids";
        context.Parameter.RenderLookup = parameterExpression => $"{queryContextParameter.Name}.ByIds({parameterExpression}){(enumerableType.IsArray ? ".ToArray()" : string.Empty)}";
    }
}
