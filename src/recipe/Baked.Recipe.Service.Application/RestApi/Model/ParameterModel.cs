using Baked.Domain.Model;

namespace Baked.RestApi.Model;

public record ParameterModel(
    TypeModel TypeModel,
    ParameterModelFrom From,
    string Id,
    Domain.Model.ParameterModel? MappedParameter = default
)
{
    public ParameterModelFrom From { get; set; } = From;
    public string Type { get; set; } = TypeModel.CSharpFriendlyFullName;
    public string Name { get; set; } = Id;
    public string InternalName { get; set; } = Id;
    public bool IsOptional { get; set; } = false;
    public object? DefaultValue { get; set; }
    public bool IsInvokeMethodParameter { get; set; } = From != ParameterModelFrom.Services;
    public bool IsHardCoded { get; set; } = false;
    public Func<string, string> LookupRenderer { get; set; } = parameterExpression => parameterExpression;
    public Func<object, string> DefaultValueRenderer { get; set; } = defaultValue => $"{defaultValue}";
    public int Order { get; set; } = 0;
    public int RoutePosition { get; set; } = 0;

    public List<string> AdditionalAttributes { get; init; } = [];

    public bool FromServices => From == ParameterModelFrom.Services;
    public bool FromRoute => From == ParameterModelFrom.Route;
    public bool FromQuery => From == ParameterModelFrom.Query;
    public bool FromBodyOrForm => From == ParameterModelFrom.BodyOrForm;

    public string RenderLookup(string parameterExpression) =>
        LookupRenderer(parameterExpression);

    public string RenderDefaultValue() =>
        DefaultValue is not null
          ? DefaultValueRenderer(DefaultValue)
          : "default";
}