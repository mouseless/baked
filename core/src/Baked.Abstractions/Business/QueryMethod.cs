using Baked.Binding;

namespace Baked.Business;

[AttributeUsage(AttributeTargets.Method)]
public class QueryMethod : Attribute, IExportOptions
{
    public bool AllParametersAreOptional { get; set; }
    public string? PrimaryParameterName { get; set; }
    string IExportOptions.Name => "Query";
}