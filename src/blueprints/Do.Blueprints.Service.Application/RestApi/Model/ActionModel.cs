using Humanizer;

namespace Do.RestApi.Model;

public record ActionModel(
    string Id,
    HttpMethod Method,
    string Route,
    ReturnModel Return,
    string FindTargetStatement,
    string InvokedMethodName
)
{
    public string Name { get; set; } = Id;
    public HttpMethod Method { get; set; } = Method;
    public string Route { get; set; } = Route;
    public Func<string, string> RoutePartStylizer { get; set; } = s => s.Kebaberize();
    public ReturnModel Return { get; set; } = Return;
    public string FindTargetStatement { get; set; } = FindTargetStatement;
    public string InvokedMethodName { get; set; } = InvokedMethodName;

    public Dictionary<string, ParameterModel> Parameter { get; init; } = [];

    public bool HasRequestBody => BodyParameters.Any();
    public IEnumerable<ParameterModel> Parameters { get => Parameter.Values; init => Parameter = value.ToDictionary(p => p.Id); }
    public IEnumerable<ParameterModel> BodyParameters => Parameters.Where(p => p.From == ParameterModelFrom.Body);
    public IEnumerable<ParameterModel> NonBodyParameters => Parameters.Where(p => p.From != ParameterModelFrom.Body);
    public IEnumerable<ParameterModel> InvokedMethodParameters => Parameters.Where(p => p.IsInvokeMethodParameter);
    public string RouteStylized => Route.Split('/').Select(part => part.StartsWith("{") ? part : RoutePartStylizer(part)).Join('/');
}
