using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Orm;
using Baked.RestApi.Model;
using Humanizer;

namespace Baked.CodingStyle.RichEntity;

public class FindTargetUsingQueryContextConvention : IDomainModelConvention<MethodModelContext>
{
    public void Apply(MethodModelContext context)
    {
        if (context.Method.Has<InitializerAttribute>()) { return; }
        if (!context.Type.TryGetMetadata(out var metadata)) { return; }
        if (!metadata.Has<EntityAttribute>()) { return; }
        if (!context.Method.TryGetSingle<ActionModelAttribute>(out var action)) { return; }

        var entityType = context.Type;
        if (!entityType.TryGetQueryContextType(context.Domain, out var queryContextType)) { return; }

        var idProperty = entityType.GetMembers().Properties["Id"];

        var target = action.Parameter[ParameterModelAttribute.TargetParameterName];
        target.Name = "id";
        target.From = ParameterModelFrom.Route;
        target.RoutePosition = 1;
        target.AdditionalAttributes.Add($"SwaggerSchema(\"Unique value to find {context.Type.Name.Humanize().ToLowerInvariant()} resource\")");
        target.Type = idProperty.PropertyType.CSharpFriendlyFullName;

        var queryContextParameter = action.AddQueryContextAsService(queryContextType);
        action.RouteParts = [entityType.Name.Pluralize(), action.Name];
        action.FindTargetStatement = queryContextParameter.BuildSingleBy("id", fromRoute: true);
    }
}