namespace Do.RestApi.Model;

public record ParameterModel(ParameterModelFrom From, string Type, string Name)
{
    public ParameterModelFrom From { get; set; } = From;
    public string Type { get; set; } = Type;
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
