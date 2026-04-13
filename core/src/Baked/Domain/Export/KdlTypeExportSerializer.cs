using Humanizer;
using KdlSharp;
using KdlSharp.Values;
using System.Globalization;

namespace Baked.Domain.Export;

public class KdlTypeExportSerializer : ITypeExportSerializer
{
    public string FileExtension => "kdl";

    public string Serialize(TypeAttributeExportModel model)
    {
        var root = new KdlNode(GetTypeName(model.Name));
        AddAttributeNodes(root, model.Attributes);

        foreach (var property in model.Properties)
        {
            var propertyNode = new KdlNode(GetPropertyName(property.Name));
            AddAttributeNodes(propertyNode, property.Attributes);

            root.AddChild(propertyNode);
        }

        foreach (var method in model.Methods)
        {
            var methodNode = new KdlNode(GetMethodName(method.Name));
            AddAttributeNodes(methodNode, method.Attributes);

            foreach (var parameter in method.Parameters)
            {
                var parameterNode = new KdlNode(GetParameterName(parameter.Name));
                AddAttributeNodes(parameterNode, parameter.Attributes);

                methodNode.AddChild(parameterNode);
            }

            root.AddChild(methodNode);
        }

        if (!root.Children.Any() && !root.Arguments.Any())
        {
            return string.Empty;
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

                    var kdlValue = GetValue(value);
                    if (kdlValue is KdlNull) { continue; }

                    childNode.AddProperty(GetPropertyName(key), kdlValue);
                }

                root.AddChild(childNode);
            }
        }
    }

    string GetTypeName(string type) =>
        $"{type.Titleize().Transform(new CultureInfo("en-US"), To.LowerCase).Kebaberize()}";

    string GetAttributeName(string attribute) =>
        $"@{attribute.Replace("Attribute", string.Empty).Titleize().Transform(new CultureInfo("en-US"), To.LowerCase).Kebaberize()}";

    string GetMethodName(string name) =>
        $"{name.Titleize().Transform(new CultureInfo("en-US"), To.LowerCase).Kebaberize()}";

    string GetParameterName(string name) =>
        $"{name[0].ToString().ToLowerInvariant()}{name[1..]}";

    string GetPropertyName(string name) =>
        $"{name[0].ToString().ToLowerInvariant()}{name[1..]}";

    protected virtual KdlValue GetValue(object value) =>
        value.GetType() switch
        {
            var type when type.IsAssignableTo(typeof(IDictionary)) => KdlNull.Instance,
            var x when x == typeof(int) => new KdlNumber(Convert.ToDecimal(value), KdlNumberFormat.Decimal),
            var x when x == typeof(decimal) => new KdlNumber((decimal)value, KdlNumberFormat.Decimal),
            var x when x == typeof(bool) => (bool)value ? KdlBoolean.True : KdlBoolean.False,
            _ => new KdlString($"{value}")
        };
}