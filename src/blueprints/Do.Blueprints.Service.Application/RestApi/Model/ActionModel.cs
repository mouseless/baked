using Do.Domain.Model;
using Humanizer;

namespace Do.RestApi.Model;

public record ActionModel(
    string Id,
    HttpMethod Method,
    string Route,
    ReturnModel Return,
    string FindTargetStatement,
    MethodModel? MethodModel = default
)
{
    public string Name { get; set; } = Id;
    public HttpMethod Method { get; set; } = Method;
    public string Route { get; set; } = Route;
    public Func<string, string> RoutePartStylizer { get; set; } = s => s.Kebaberize();
    public ReturnModel Return { get; set; } = Return;
    public string FindTargetStatement { get; set; } = FindTargetStatement;
    public bool UseForm { get; set; } = false;
    public int Order { get; set; } = 0;

    public List<string> AdditionalAttributes { get; init; } = [];
    public Dictionary<string, ParameterModel> Parameter { get; init; } = [];
    public List<string> PreparationStatements { get; init; } = [];

    public bool HasBodyOrForm => BodyOrFormParameters.Any();
    public IEnumerable<ParameterModel> Parameters { get => Parameter.Values; init => Parameter = value.ToDictionary(p => p.Id); }
    public IEnumerable<ParameterModel> ActionParameters => Parameters.Where(p => !p.IsHardCoded);
    public IEnumerable<ParameterModel> BodyOrFormParameters => ActionParameters.Where(p => p.From == ParameterModelFrom.BodyOrForm);
    public IEnumerable<ParameterModel> NonBodyOrFormParameters => ActionParameters.Where(p => p.From != ParameterModelFrom.BodyOrForm);
    public IEnumerable<ParameterModel> RouteParameters => Parameters.Where(p => p.From == ParameterModelFrom.Route).OrderBy(p => p.RoutePosition);
    public IEnumerable<ParameterModel> InvokedMethodParameters => Parameters.Where(p => p.IsInvokeMethodParameter).OrderBy(p => p.Order);

    public string RouteStylized
    {
        get
        {
            var routeParts = Route.Split('/').Select(part => RoutePartStylizer(part)).ToList();
            foreach (var routeParameter in RouteParameters)
            {
                var index = routeParameter.RoutePosition > routeParts.Count ? routeParts.Count : routeParameter.RoutePosition;
                routeParts.Insert(index, routeParameter.GetRouteString());
            }

            return routeParts.Join('/');
        }
    }
}