using Do.Domain.Model;
using Humanizer;

namespace Do.RestApi.Model;

public record ActionModel(
    string Id,
    HttpMethod Method,
    List<string> RouteParts,
    ReturnModel Return,
    string FindTargetStatement,
    MethodModel? MappedMethod = default
)
{
    public string Name { get; set; } = Id;
    public HttpMethod Method { get; set; } = Method;
    public List<string> RouteParts { get; set; } = RouteParts;
    public Func<string, string> RoutePartStylizer { get; set; } = s => s.Kebaberize();
    public ReturnModel Return { get; set; } = Return;
    public string FindTargetStatement { get; set; } = FindTargetStatement;
    public bool UseForm { get; set; } = false;
    public int Order { get; set; } = 0;

    public List<string> AdditionalAttributes { get; init; } = [];
    public Dictionary<string, ParameterModel> Parameter { get; init; } = [];
    public List<string> PreparationStatements { get; init; } = [];

    public bool HasBody => !UseForm && BodyParameters.Any();

    public IEnumerable<ParameterModel> Parameters { get => Parameter.Values; init => Parameter = value.ToDictionary(p => p.Id); }

    IEnumerable<ParameterModel> ActionParameters => Parameters.Where(p => !p.IsHardCoded).OrderBy(p => p.Order).OrderBy(p => p.IsOptional ? 1 : -1);
    IEnumerable<ParameterModel> RouteParameters => Parameters.Where(p => p.From == ParameterModelFrom.Route).OrderBy(p => p.RoutePosition);
    IEnumerable<ParameterModel> NonServiceParameters => ActionParameters.Where(p => p.From != ParameterModelFrom.Services);

    public IEnumerable<ParameterModel> BodyParameters => !UseForm ? ActionParameters.Where(p => p.From == ParameterModelFrom.BodyOrForm) : [];
    public IEnumerable<ParameterModel> ServiceParameters => ActionParameters.Where(p => p.From == ParameterModelFrom.Services);
    public IEnumerable<ParameterModel> NonBodyParameters => UseForm ? NonServiceParameters : NonServiceParameters.Where(p => p.From != ParameterModelFrom.BodyOrForm);
    public IEnumerable<ParameterModel> InvokedMethodParameters => Parameters.Where(p => p.IsInvokeMethodParameter);

    public string GetRoute()
    {
        var routeParts = RouteParts.Select(part => RoutePartStylizer(part)).ToList();
        foreach (var routeParameter in RouteParameters)
        {
            var index = routeParameter.RoutePosition > routeParts.Count ? routeParts.Count : routeParameter.RoutePosition;
            routeParts.Insert(index, routeParameter.GetRouteString());
        }

        return routeParts.Join('/');
    }
}