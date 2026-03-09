// AI-GEN
//
// swashbuckle c#:
//
// create a document filter that removes any schema that isn't being used in
// any of paths parameters, request or response properties and properties of
// other schemas.
//
// class name: `RemoveUnusedSchemasDocumentFilter`
//
// namespace: `Baked.Binding.Rest`.
//
// smallest code possible with minimal nesting and performant code. (use
// hashset and/or dictionary etc.)
//
// always use brackets for if, for and while.  never single line if, for or
// while.
//
// only respond with code snippet.
//
// After the .NET 10 update, the code was reviewed again and the errors were fixed.
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Binding.Rest;

public class RemoveUnusedSchemasDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (swaggerDoc.Components?.Schemas is not { Count: > 0 } schemas)
        {
            return;
        }

        var used = new HashSet<string>();
        CollectFromPaths(swaggerDoc, used);
        CollectFromSchemaProperties(schemas, used);

        foreach (var key in schemas.Keys.Except(used).ToList())
        {
            schemas.Remove(key);
        }
    }

    static void CollectFromPaths(OpenApiDocument doc, HashSet<string> used)
    {
        if (doc.Paths is null) { return; }

        foreach (var path in doc.Paths.Values)
        {
            if (path.Operations is null) { continue; }

            foreach (var op in path.Operations.Values)
            {
                if (op.Parameters is not null)
                {
                    foreach (var param in op.Parameters)
                    {
                        CollectFromSchema(param.Schema, used);
                    }
                }

                if (op.RequestBody?.Content is not null)
                {
                    foreach (var media in op.RequestBody.Content.Values)
                    {
                        CollectFromSchema(media.Schema, used);
                    }
                }

                if (op.Responses is null) { continue; }

                foreach (var response in op.Responses.Values)
                {
                    if (response.Content is null) { continue; }

                    foreach (var media in response.Content.Values)
                    {
                        CollectFromSchema(media.Schema, used);
                    }
                }
            }
        }
    }

    static void CollectFromSchemaProperties(IDictionary<string, IOpenApiSchema> schemas, HashSet<string> used)
    {
        foreach (var schema in schemas.Values)
        {
            if (schema.Properties is null) { continue; }

            foreach (var prop in schema.Properties.Values)
            {
                CollectFromSchema(prop, used);
            }
        }
    }

    static void CollectFromSchema(IOpenApiSchema? schema, HashSet<string> used)
    {
        if (schema is null) { return; }

        if (schema is OpenApiSchemaReference { Reference.Id: { } referenceId })
        {
            used.Add(referenceId);
            return;
        }

        if (schema.Properties is not null)
        {
            foreach (var prop in schema.Properties.Values)
            {
                CollectFromSchema(prop, used);
            }
        }

        CollectFromSchema(schema.Items, used);

        foreach (var sub in schema.AllOf ?? [])
        {
            CollectFromSchema(sub, used);
        }

        foreach (var sub in schema.AnyOf ?? [])
        {
            CollectFromSchema(sub, used);
        }

        foreach (var sub in schema.OneOf ?? [])
        {
            CollectFromSchema(sub, used);
        }
    }
}