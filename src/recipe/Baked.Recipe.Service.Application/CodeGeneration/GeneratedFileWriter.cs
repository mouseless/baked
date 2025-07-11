using System.Text;

namespace Baked.CodeGeneration;

public class GeneratedFileWriter(GeneratedFileDescriptor _descriptor)
{
    public static bool RequiresUpdate(string content, string filePath, string hashFilePath)
    {
        if (!Path.Exists(hashFilePath) || !Path.Exists(filePath)) { return true; }

        using (StreamReader reader = new(hashFilePath))
        {
            string hashValue = reader.ReadToEnd();

            return content.ToSHA256().ToUtf8String() != hashValue;
        }
    }

    public static void CreateHashFile(string content, string hashFilePath)
    {
        using (var fs = new FileStream(hashFilePath, FileMode.Create))
        {
            fs.Write(content.ToSHA256());
        }
    }

    public virtual string Create(string location,
        Func<GeneratedFileDescriptor, string>? fileNameBuilder = default
    )
    {
        fileNameBuilder ??= d => _descriptor.Name;

        var outdir = GetOutputDirectory(location, _descriptor.Outdir);
        var filePath = Path.Combine(outdir, $"{fileNameBuilder(_descriptor)}.{_descriptor.Extension}");
        var hashFilePath = $"{filePath}.hash";

        if (!_descriptor.ForceUpdate && !RequiresUpdate(_descriptor.Content, filePath, hashFilePath))
        {
            return filePath;
        }

        if (!Directory.Exists(outdir))
        {
            Directory.CreateDirectory(outdir);
        }

        using var file = new FileStream(filePath, FileMode.Create);
        file.Write(Encoding.UTF8.GetBytes(_descriptor.Content));

        if (!_descriptor.ForceUpdate)
        {
            using var hashfile = new FileStream(hashFilePath, FileMode.Create);
            hashfile.Write(_descriptor.Content.ToSHA256());
        }

        return filePath;
    }

    string GetOutputDirectory(string location,
        string? outdir = default
    )
    {
        if (outdir == null) { return location; }

        outdir = Path.Combine(outdir.Split('/'));
        if (Path.IsPathRooted(outdir)) { return outdir; }

        return Path.Combine(location, outdir);
    }
}