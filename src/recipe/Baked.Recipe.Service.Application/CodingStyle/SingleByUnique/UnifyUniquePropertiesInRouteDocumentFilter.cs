using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.CodingStyle.SingleByUnique;

public class UnifyUniquePropertiesInRouteDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var unifiedActions = new Dictionary<string, UnifiedAction>();
        foreach (var action in context.ApiDescriptions)
        {
            if (action.GroupName is null) { continue; }
            if (!action.TryGetMethodInfo(out var method)) { continue; }

            var unique = (SingleByUniqueAttribute?)Attribute.GetCustomAttribute(method, typeof(SingleByUniqueAttribute));
            if (unique is null) { continue; }

            var route = (RouteAttribute?)Attribute.GetCustomAttribute(method, typeof(RouteAttribute));
            if (route is null) { continue; }

            if (!unifiedActions.TryGetValue(action.GroupName, out var unifiedAction))
            {
                unifiedActions.Add(action.GroupName, unifiedAction = new($"/{route.Template.Split('/').First()}"));
            }

            unifiedAction.PathsToRemove.Add($"/{route.Template.Replace(":guid", string.Empty)}");
            unifiedAction.UniquePropertyNames.Add(unique.PropertyName);
        }

        foreach (var (groupName, unifiedAction) in unifiedActions)
        {
            OpenApiOperation? operationTemplate = null;
            foreach (var pathToRemove in unifiedAction.PathsToRemove)
            {
                if (!swaggerDoc.Paths.Remove(pathToRemove, out var removed)) { continue; }
                if (!removed.Operations.TryGetValue(OperationType.Get, out operationTemplate)) { continue; }

                operationTemplate.Parameters.Clear();
            }

            if (!swaggerDoc.Paths.TryGetValue(unifiedAction.Path, out var unifiedPath))
            {
                swaggerDoc.Paths.Add(unifiedAction.Path, unifiedPath = new());
            }

            var operation = operationTemplate is not null
                ? operationTemplate
                : new() { Tags = [new() { Name = groupName }] };

            operation.Parameters.Add(new() { In = ParameterLocation.Path, Name = unifiedAction.PathParameter, Required = true });
            unifiedPath.AddOperation(OperationType.Get, operation);
        }
    }

    record UnifiedAction(string BasePath)
    {
        public List<string> PathsToRemove { get; } = [];
        public List<string> UniquePropertyNames { get; } = [];

        public string Path => $"{BasePath}/{{{PathParameter}}}";
        public string PathParameter => UniquePropertyNames.OrderBy(s => s == "Id" ? -1 : 0).Join("Or").Camelize();
    }
}