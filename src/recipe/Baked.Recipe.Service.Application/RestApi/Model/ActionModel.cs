using Humanizer;

namespace Baked.RestApi.Model;

[AttributeUsage(AttributeTargets.Method)]
public class ActionModel(string method, string[] routeParts,
    string[]? additionalAttributes = default,
    string[]? preparationStatements = default
) : Attribute
{
    public string Id { get; private set; } = default!;
    public string Name { get; set; } = default!;
    public HttpMethod Method { get; set; } = default!;
    public List<string> RouteParts { get; set; } = default!;
    public Func<string, string> RoutePartStylizer { get; set; } = default!;
    public string ReturnType { get; set; } = default!;
    public bool ReturnIsAsync { get; set; } = default!;
    public bool ReturnIsVoid { get; set; } = default!;
    public Func<string, string> ReturnResultRenderer { get; set; } = default!;
    public string FindTargetStatement { get; set; } = default!;
    public bool UseForm { get; set; } = default;
    public bool UseRequestClassForBody { get; set; } = default!;
    public int Order { get; set; } = default!;
    public List<string> AdditionalAttributes { get; set; } = default!;
    public List<string> PreparationStatements { get; set; } = default!;
    public Dictionary<string, ParameterModel> Parameter { get; private set; } = default!;

    public bool HasBody => !UseForm && BodyParameters.Any();
    public IEnumerable<ParameterModel> Parameters => Parameter.Values;
    IEnumerable<ParameterModel> ActionParameters => Parameters.Where(p => !p.IsHardCoded).OrderBy(p => p.Order).ThenBy(p => p.IsOptional ? 1 : -1);
    IEnumerable<ParameterModel> RouteParameters => Parameters.Where(p => p.From == ParameterModelFrom.Route).OrderBy(p => p.RoutePosition);
    IEnumerable<ParameterModel> NonServiceParameters => ActionParameters.Where(p => p.From != ParameterModelFrom.Services);
    public IEnumerable<ParameterModel> BodyParameters => ActionParameters.Where(p => p.From == ParameterModelFrom.BodyOrForm);
    public IEnumerable<ParameterModel> ServiceParameters => ActionParameters.Where(p => p.From == ParameterModelFrom.Services);
    public IEnumerable<ParameterModel> NonBodyParameters => NonServiceParameters.Where(p => p.From != ParameterModelFrom.BodyOrForm);
    public IEnumerable<ParameterModel> InvokedMethodParameters => Parameters.Where(p => p.IsInvokeMethodParameter);

    internal void Init(string id, string defaultReturnType, bool defaultReturnIsAsync, bool defaultReturnIsVoid, IEnumerable<ParameterModel> parameters)
    {
        Id = id;
        Name ??= id;
        Method ??= HttpMethod.Parse(method);
        RouteParts ??= [.. routeParts];
        RoutePartStylizer ??= s => s.Kebaberize();
        ReturnType ??= defaultReturnType;
        ReturnIsAsync ??= defaultReturnIsAsync;
        ReturnIsVoid ??= defaultReturnIsVoid;
        ReturnResultRenderer ??= resultExpression => resultExpression;
        FindTargetStatement ??= "target";
        UseForm ??= false;
        UseRequestClassForBody ??= true;
        Order ??= 0;
        AdditionalAttributes ??= [.. additionalAttributes];
        PreparationStatements ??= [.. preparationStatements];
        Parameter ??= parameters.ToDictionary(p => p.Id);
    }

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

    public string RenderReturnResult(string resultExpression) =>
        ReturnResultRenderer(resultExpression);
}