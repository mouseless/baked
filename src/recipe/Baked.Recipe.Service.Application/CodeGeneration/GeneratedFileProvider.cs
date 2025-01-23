namespace Baked.CodeGeneration;

public class GeneratedFileProvider : Dictionary<string, string>
{
    public string GetFileContent(string key)
    {
        var result = string.Empty;
        using (var file = new FileStream(this[key], FileMode.Open))
        {
            using var reader = new StreamReader(file);
            result = reader.ReadToEnd();
        }

        return result;
    }
}