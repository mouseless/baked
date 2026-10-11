using Baked.Binding;

namespace Baked.CodingStyle.CommandViaMethodName;

[AttributeUsage(AttributeTargets.Method)]
public class CommandMethod : Attribute, IExportOptions
{
    string IExportOptions.Name => "Command";
}