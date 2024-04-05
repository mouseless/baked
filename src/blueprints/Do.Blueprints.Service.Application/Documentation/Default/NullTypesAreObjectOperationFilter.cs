using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Do.Documentation.Default;

public class NullTypesAreObjectOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        foreach (var schema in context.SchemaRepository.Schemas.Values)
        {
            foreach (var property in schema.Properties.Values)
            {
                if (property.Type is null)
                {
                    property.Type = "object";
                }
            }
        }
    }
}