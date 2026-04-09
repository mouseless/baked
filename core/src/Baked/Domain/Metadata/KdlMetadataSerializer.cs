using Humanizer;
using KdlSharp;
using KdlSharp.Values;
using System.Globalization;

namespace Baked.Domain.Metadata;

public class KdlMetadataSerializer : ITypeMetadataSerializer
{
    public string FileType => "kdl";

    public string Serialize(TypeMetadataModel model)
    {
        var root = new KdlNode(model.Name);
        AddAttributeNodes(root, model.Attributes);

        foreach (var property in model.Properties)
        {
            var childNode = new KdlNode(GetPropertyName(property.Name));
            AddAttributeNodes(childNode, property.Attributes);

            root.AddChild(childNode);
        }

        foreach (var method in model.Methods)
        {
            var childNode = new KdlNode(GetPropertyName(method.Name));
            AddAttributeNodes(childNode, method.Attributes);

            root.AddChild(childNode);
        }

        return root.ToKdlString(new()
        {
            Indentation = "  ",
            Newline = Environment.NewLine,
            PreserveStringTypes = true,
        });
    }

    void AddAttributeNodes(KdlNode root, List<AttributeMetadataModel> attributes)
    {
        foreach (var attribute in attributes)
        {
            if (attribute.Values is null || !attribute.Values.Any())
            {
                root.Arguments.Add(new KdlString(GetAttributeName(attribute.Type), KdlStringType.Identifier));
            }
            else
            {
                var childNode = new KdlNode(GetAttributeName(attribute.Type));
                foreach (var (key, value) in attribute.Values)
                {
                    if (value is null) { continue; }

                    if (value.GetType().IsArray || value.GetType().IsAssignableTo(typeof(ICollection)))
                    {
                        childNode.AddProperty(GetPropertyName(key), string.Join(", ", (IEnumerable<object>)value));
                    }
                    else
                    {
                        childNode.AddProperty(GetPropertyName(key), new KdlString($"{value}"));
                    }
                }

                root.AddChild(childNode);
            }
        }
    }

    string GetAttributeName(string type) =>
        $"@{type.Replace("Attribute", string.Empty).Titleize().Transform(new CultureInfo("en-US"), To.LowerCase).Kebaberize()}";

    string GetPropertyName(string name) =>
        $"{name[0].ToString().ToLowerInvariant()}{name[1..]}";
}