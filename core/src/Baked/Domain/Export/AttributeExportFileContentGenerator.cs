using System.Text;

namespace Baked.Domain.Export;

public class AttributeExportFileContentGenerator(AttributeExportFileContentGenerator.Options _options)
{
    public Dictionary<string, string> Generate(ExportSetModel model)
    {
        var contents = new Dictionary<string, StringBuilder>();
        foreach (var type in model.Types)
        {
            var content = _options.Serializer.Serialize(type).Trim();
            if (content.Length == 0)
            {
                content = $"// No exportable data exists for '{type.Id}'";
            }

            var fileName = _options.ContentGroupName(type);
            if (!contents.TryGetValue(fileName, out StringBuilder? value))
            {
                value = new();
                contents[fileName] = value;
            }

            value.AppendLine(content);
            value.AppendLine();
        }

        return contents.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
    }

    public class Options
    {
        public ITypeExportSerializer Serializer { get; set; } = new KdlTypeExportSerializer();
        public Func<TypeExportModel, string> ContentGroupName { get; set; } = type => type.GroupName;
    }
}