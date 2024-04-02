using Do.Domain.Model;

namespace Do.RestApi.Model;

public record ParameterModel(TypeModel TypeModel, ParameterModelFrom From, string Name)
{
    public ParameterModelFrom From { get; set; } = From;
    public string Type { get; set; } = TypeModel.CSharpFriendlyFullName;
    public string Name { get; set; } = Name;
    public string InternalName { get; set; } = Name;
    public bool IsInvokeMethodParameter { get; set; } = true;
    public Func<string, string> RenderLookup { get; set; } = parameterExpression => parameterExpression;

    public bool FromServices => From == ParameterModelFrom.Services;
    public bool FromRoute => From == ParameterModelFrom.Route;
    public bool FromQuery => From == ParameterModelFrom.Query;
    public bool FromForm => From == ParameterModelFrom.Form;
    public bool FromBody => From == ParameterModelFrom.Body;
}
