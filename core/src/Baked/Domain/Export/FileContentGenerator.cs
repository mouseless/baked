using System.Text;

namespace Baked.Domain.Export;

public class FileContentGenerator(ITypeExportSerializer _serializer, Func<TypeExportModel, string> _contentGroupName)
{
    public Dictionary<string, string> Generate(ExportSetModel model)
    {
        var contents = new Dictionary<string, StringBuilder>();
        foreach (var type in model.Types)
        {
            var content = _serializer.Serialize(type).Trim();
            if (content.Length == 0)
            {
                content = $"// No exportable data exists for '{type.Id}'";
            }

            var fileName = _contentGroupName(type);
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
}