namespace Baked.RestApi.Model;

[AttributeUsage(AttributeTargets.Parameter)]
public class ParameterModel(ParameterModelFrom From,
    string[]? additionalAttributes = default
) : Attribute
{
    public ParameterModel(string id, string type, ParameterModelFrom @from,
        IEnumerable<string>? additionalAttributes = default
    ) : this(@from,
        additionalAttributes: additionalAttributes?.ToArray()
    )
    {
        Init(id, type, false, null);

        ManuallyAdded = true;
    }

    public string Id { get; private set; } = default!;
    public ParameterModelFrom From { get; set; } = From;
    public string Type { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string InternalName { get; set; } = default!;

    /// <summary>
    /// Do NOT set this property directly from the attribute definition, e.g.,
    /// `[ParameterModel(..., IsOptional = true, ...)]`. Initial value is always
    /// overridden by the value comes from reflection.
    ///
    /// Use conventions to set a custom value.
    /// </summary>
    public bool IsOptional { get; set; } = false;

    public object? DefaultValue { get; set; }
    public bool IsInvokeMethodParameter { get; set; } = From != ParameterModelFrom.Services;
    public bool IsHardCoded { get; set; } = false;
    public Func<string, string> LookupRenderer { get; set; } = parameterExpression => parameterExpression;
    public Func<object, string> DefaultValueRenderer { get; set; } = defaultValue => $"{defaultValue}";
    public int Order { get; set; } = 0;
    public int RoutePosition { get; set; } = 0;
    public List<string> AdditionalAttributes { get; } = [.. additionalAttributes ?? []];
    public bool ManuallyAdded { get; } = false;

    public bool FromServices => From == ParameterModelFrom.Services;
    public bool FromRoute => From == ParameterModelFrom.Route;
    public bool FromQuery => From == ParameterModelFrom.Query;
    public bool FromBodyOrForm => From == ParameterModelFrom.BodyOrForm;

    internal ParameterModel Init(string id, string type, bool isOptional, object? defaultValue)
    {
        Id = id;
        Type ??= type;
        Name ??= id;
        InternalName ??= id;
        IsOptional = isOptional;
        DefaultValue ??= defaultValue;

        return this;
    }

    public string RenderLookup(string parameterExpression) =>
        LookupRenderer(parameterExpression);

    public string RenderDefaultValue() =>
        DefaultValue is not null
          ? DefaultValueRenderer(DefaultValue)
          : "default";
}