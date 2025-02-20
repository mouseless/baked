namespace Baked.RestApi;

[AttributeUsage(AttributeTargets.Parameter)]
public class ParameterModelAttribute(ParameterModelFrom @from, string id,
    string[]? additionalAttributes = default
) : Attribute
{
    public ParameterModelFrom From { get; set; } = @from;
    public string Id { get; } = id;
    public string? Type { get; set; }
    public string? Name { get; set; }
    public string? InternalName { get; set; }
    public bool? IsOptional { get; set; }
    public object? DefaultValue { get; set; }
    public bool? IsInvokeMethodParameter { get; set; }
    public bool? IsHardCoded { get; set; }
    public Func<string, string>? LookupRenderer { get; set; }
    public Func<object, string>? DefaultValueRenderer { get; set; }
    public int? Order { get; set; }
    public int? RoutePosition { get; set; }
    public List<string> AdditionalAttributes { get; set; } = [.. additionalAttributes ?? []];
}