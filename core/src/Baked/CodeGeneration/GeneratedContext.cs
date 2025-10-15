using Newtonsoft.Json;
using System.Reflection;

namespace Baked.CodeGeneration;

public class GeneratedContext
{
    public Dictionary<string, Assembly> Assemblies { get; } = [];
    public Dictionary<string, string> Files { get; } = [];

    public string ReadFile(string key)
    {
        using var file = new FileStream(Files[key], FileMode.Open);
        using var reader = new StreamReader(file);

        return reader.ReadToEnd();
    }

    public T? ReadFileAsJson<T>() where T : notnull =>
        JsonConvert.DeserializeObject<T>(ReadFile(typeof(T).Name));
}