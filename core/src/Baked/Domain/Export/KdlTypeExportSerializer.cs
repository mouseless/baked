using Humanizer;
using KdlSharp;
using KdlSharp.Values;
using System.Globalization;

namespace Baked.Domain.Export;

public class KdlTypeExportSerializer : ITypeExportSerializer
{
    public string FileExtension => "kdl";

    public string Serialize(TypeExportModel model)
    {
        var root = new KdlNode(model.Name);
        AddAttributeNodes(root, model.Attributes);

        foreach (var property in model.Properties)
        {
            var propertyNode = new KdlNode(GetPropertyName(property.Name));
            AddAttributeNodes(propertyNode, property.Attributes);

            root.AddChild(propertyNode);
        }

        foreach (var method in model.Methods)
        {
            var methodNode = new KdlNode(GetPropertyName(method.Name));
            AddAttributeNodes(methodNode, method.Attributes);

            foreach (var parameter in method.Parameters)
            {
                var parameterNode = new KdlNode(GetPropertyName(parameter.Name));
                AddAttributeNodes(parameterNode, parameter.Attributes);

                methodNode.AddChild(parameterNode);
            }

            root.AddChild(methodNode);
        }

        return root.ToKdlString(new()
        {
            Indentation = "  ",
            Newline = Environment.NewLine,
            PreserveStringTypes = true,
        });
    }

    void AddAttributeNodes(KdlNode root, List<AttributeExportModel> attributes)
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

                    childNode.AddProperty(GetPropertyName(key), GetValue(value));
                }

                root.AddChild(childNode);
            }
        }
    }

    string GetAttributeName(string type) =>
        $"@{type.Replace("Attribute", string.Empty).Titleize().Transform(new CultureInfo("en-US"), To.LowerCase).Kebaberize()}";

    string GetPropertyName(string name) =>
        $"{name[0].ToString().ToLowerInvariant()}{name[1..]}";

    KdlValue GetValue(object value) =>
        value.GetType() switch
        {
            var x when x == typeof(int) => new KdlNumber(Convert.ToDecimal(value), KdlNumberFormat.Decimal),
            var x when x == typeof(decimal) => new KdlNumber((decimal)value, KdlNumberFormat.Decimal),
            var x when x == typeof(bool) => (bool)value ? KdlBoolean.True : KdlBoolean.False,
            _ => new KdlString($"{value}")
        };
}