using Newtonsoft.Json;
using System.Reflection;

namespace Baked.CodeGeneration;

public class GeneratedContext
{
    public Dictionary<string, Assembly> Assemblies { get; } = [];
    public Dictionary<string, string> Files { get; } = [];

    public string GetFileContent(string key)
    {
        var result = string.Empty;
        using (var file = new FileStream(Files[key], FileMode.Open))
        {
            using var reader = new StreamReader(file);
            result = reader.ReadToEnd();
        }

        return result;
    }

    public T? LoadFromFile<T>() where T : notnull
    {
        var result = GetFileContent(typeof(T).Name);

        return JsonConvert.DeserializeObject<T>(result);
    }
}