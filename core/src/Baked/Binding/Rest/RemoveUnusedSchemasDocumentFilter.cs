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
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Baked.Binding.Rest
{
    public class RemoveUnusedSchemasDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument doc, DocumentFilterContext context)
        {
            if (doc.Components?.Schemas == null)
            {
                return;
            }

            var schemas = doc.Components.Schemas;
            var used = new HashSet<string>();
            var stack = new Stack<OpenApiSchema>();

            void Add(OpenApiSchema s)
            {
                if (s == null)
                {
                    return;
                }

                if (s.Reference?.Id != null)
                {
                    if (used.Add(s.Reference.Id) && schemas.TryGetValue(s.Reference.Id, out var target))
                    {
                        stack.Push(target);
                    }
                }

                stack.Push(s);
            }

            foreach (var path in doc.Paths.Values)
            {
                foreach (var op in path.Operations.Values)
                {
                    foreach (var p in op.Parameters)
                    {
                        Add(p.Schema);
                    }

                    if (op.RequestBody != null)
                    {
                        foreach (var c in op.RequestBody.Content.Values)
                        {
                            Add(c.Schema);
                        }
                    }

                    foreach (var r in op.Responses.Values)
                    {
                        foreach (var c in r.Content.Values)
                        {
                            Add(c.Schema);
                        }
                    }
                }
            }

            while (stack.Count > 0)
            {
                var s = stack.Pop();

                if (s.Reference?.Id != null)
                {
                    if (used.Add(s.Reference.Id) && schemas.TryGetValue(s.Reference.Id, out var target))
                    {
                        stack.Push(target);
                        continue;
                    }
                }

                foreach (var p in s.Properties.Values)
                {
                    Add(p);
                }

                Add(s.Items);

                foreach (var a in s.AllOf)
                {
                    Add(a);
                }

                foreach (var a in s.OneOf)
                {
                    Add(a);
                }

                foreach (var a in s.AnyOf)
                {
                    Add(a);
                }

                Add(s.AdditionalProperties);
            }

            foreach (var key in schemas.Keys.ToList())
            {
                if (!used.Contains(key))
                {
                    schemas.Remove(key);
                }
            }
        }
    }
}