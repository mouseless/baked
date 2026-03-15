// AI-GEN(Claude AI Sonnet 4.6)
//
// swashbuckle c# (.net 10 / Microsoft.OpenApi v2):
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
// always use brackets for if, for and while. never single line if, for or
// while.
//
// use `IOpenApiSchema` instead of `OpenApiSchema`. use `OpenApiSchemaReference`
// for reference detection via `Reference.Id`. use null-safe path traversal with
// `.Where(x => x.Operations is not null).SelectMany(x => x.Operations!.Values)`
// instead of `?? []` on ValueCollections. combine AllOf/AnyOf/OneOf traversal
// with `.Concat()`. use `HashSet<string>.Contains` for unused key filtering
// instead of `Except()`.
//
// only respond with code snippet.
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
        CollectFromPaths(swaggerDoc, schemas, used);

        var unused = schemas.Keys.Where(k => !used.Contains(k)).ToList();
        foreach (var key in unused)
        {
            schemas.Remove(key);
        }
    }

    static void CollectFromPaths(OpenApiDocument doc, IDictionary<string, IOpenApiSchema> schemas, HashSet<string> used)
    {
        var operations = doc.Paths?.Values
            .Where(path => path.Operations is not null)
            .SelectMany(path => path.Operations!.Values);

        if (operations is null) { return; }

        foreach (var op in operations)
        {
            foreach (var param in op.Parameters ?? [])
            {
                CollectFromSchema(param.Schema, schemas, used);
            }

            foreach (var media in op.RequestBody?.Content?.Values ?? [])
            {
                CollectFromSchema(media.Schema, schemas, used);
            }

            foreach (var media in op.Responses?.Values.SelectMany(r => r.Content?.Values ?? []) ?? [])
            {
                CollectFromSchema(media.Schema, schemas, used);
            }
        }
    }

    static void CollectFromSchema(IOpenApiSchema? schema, IDictionary<string, IOpenApiSchema> schemas, HashSet<string> used)
    {
        if (schema is null) { return; }

        if (schema is OpenApiSchemaReference { Reference.Id: { } referenceId })
        {
            if (!used.Add(referenceId)) { return; }
            if (!schemas.TryGetValue(referenceId, out var referencedSchema)) { return; }

            CollectFromSchema(referencedSchema, schemas, used);
            return;
        }

        if (schema.Properties is not null)
        {
            foreach (var prop in schema.Properties.Values)
            {
                CollectFromSchema(prop, schemas, used);
            }
        }

        CollectFromSchema(schema.Items, schemas, used);

        foreach (var sub in (schema.AllOf ?? []).Concat(schema.AnyOf ?? []).Concat(schema.OneOf ?? []))
        {
            CollectFromSchema(sub, schemas, used);
        }
    }
}