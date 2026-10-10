using Humanizer;

namespace Baked.RestApi.Model;

[AttributeUsage(AttributeTargets.Method)]
public class ApiAction(
    string? method = default,
    string[]? routeParts = default,
    string[]? additionalAttributes = default,
    string[]? preparationStatements = default
) : Attribute
{
    public ApiAction(string id, IEnumerable<string> routeParts, string returnType, bool returnIsAsync, bool returnIsVoid, IEnumerable<ApiParameter> parameters)
      : this()
    {
        Init(id, routeParts, returnType, returnIsAsync, returnIsVoid, parameters);

        Orphan = true;
    }

    public string Id { get; private set; } = default!;
    public string Name { get; set; } = default!;
    public HttpMethod Method { get; set; } = HttpMethod.Parse(method ?? "Post");
    public List<string> RouteParts { get; set; } = [.. routeParts ?? []];
    public Func<string, string> RoutePartStylizer { get; set; } = s => s.Kebaberize();
    public string ReturnType { get; set; } = default!;

    /// <summary>
    /// Do NOT set this property directly from the attribute definition, e.g.,
    /// `[ApiAction(..., ReturnIsAsync = true, ...)]`. Initial value is
    /// always overridden by the value comes from reflection.
    ///
    /// Use conventions to set a custom value.
    /// </summary>
    public bool ReturnIsAsync { get; set; } = default!;

    /// <summary>
    /// Do NOT set this property directly from the attribute definition, e.g.,
    /// `[ApiAction(..., ReturnIsVoid = true, ...)]`. Initial value is always
    /// overridden by the value comes from reflection.
    ///
    /// Use conventions to set a custom value.
    /// </summary>
    public bool ReturnIsVoid { get; set; } = default!;

    /// <summary>
    /// Do NOT set this property directly from the attribute definition, e.g.,
    /// `[ApiAction(..., InvocationIsAsync = true, ...)]`. Initial value is
    /// always overridden by the value comes from reflection.
    ///
    /// Use conventions to set a custom value.
    /// </summary>
    public bool InvocationIsAsync { get; set; } = default!;

    public Func<string, string> ReturnResultRenderer { get; set; } = resultExpression => resultExpression;
    public string FindTargetStatement { get; set; } = ApiParameter.TargetParameterName;
    public bool UseForm { get; set; } = false;
    public bool UseRequestClassForBody { get; set; } = true;
    public int Order { get; set; } = 0;
    public List<string> AdditionalAttributes { get; } = [.. additionalAttributes ?? []];
    public List<string> PreparationStatements { get; } = [.. preparationStatements ?? []];
    public Dictionary<string, ApiParameter> Parameter { get; private set; } = default!;
    public bool Orphan { get; } = false;
    internal bool Initialized { get; private set; } = false;

    public bool HasBody => !UseForm && BodyParameters.Any();
    public IEnumerable<ApiParameter> Parameters => Parameter.Values;
    IEnumerable<ApiParameter> ActionParameters => Parameters.Where(p => !p.IsHardCoded).OrderBy(p => p.Order).ThenBy(p => p.IsOptional ? 1 : -1);
    IEnumerable<ApiParameter> RouteParameters => ActionParameters.Where(p => p.From == ParameterModelFrom.Route).OrderBy(p => p.RoutePosition);
    IEnumerable<ApiParameter> NonServiceParameters => ActionParameters.Where(p => p.From != ParameterModelFrom.Services);
    public IEnumerable<ApiParameter> BodyParameters => ActionParameters.Where(p => p.From == ParameterModelFrom.BodyOrForm);
    public IEnumerable<ApiParameter> ServiceParameters => ActionParameters.Where(p => p.From == ParameterModelFrom.Services);
    public IEnumerable<ApiParameter> NonBodyParameters => NonServiceParameters.Where(p => p.From != ParameterModelFrom.BodyOrForm);
    public IEnumerable<ApiParameter> InvokedMethodParameters => Parameters.Where(p => p.IsInvokeMethodParameter);

    internal ApiAction Init(string id, IEnumerable<string> routeParts, string returnType, bool returnIsAsync, bool returnIsVoid, IEnumerable<ApiParameter> parameters)
    {
        if (Initialized) { throw new($"Cannot initialize, already initialized: {Id}"); }

        Id = id;
        Name ??= id;
        RouteParts = RouteParts.Any() ? RouteParts : [.. routeParts];
        ReturnType ??= returnType;
        ReturnIsAsync = returnIsAsync;
        ReturnIsVoid = returnIsVoid;
        InvocationIsAsync = returnIsAsync;
        Parameter ??= parameters.ToDictionary(p => p.Id);
        Initialized = true;

        return this;
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

    public string GetRoutePart(int index) =>
        RoutePartStylizer(RouteParts[index]);

    public string RenderReturnResult(string resultExpression) =>
        ReturnResultRenderer(resultExpression);
}