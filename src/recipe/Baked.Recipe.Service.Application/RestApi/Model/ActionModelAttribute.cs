using Humanizer;

namespace Baked.RestApi.Model;

[AttributeUsage(AttributeTargets.Method)]
public class ActionModelAttribute(
    string? method = default,
    string[]? routeParts = default,
    string[]? additionalAttributes = default,
    string[]? preparationStatements = default
) : Attribute
{
    public ActionModelAttribute(string id, IEnumerable<string> routeParts, string returnType, bool returnIsAsync, bool returnIsVoid, IEnumerable<ParameterModelAttribute> parameters)
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
    /// `[ActionModel(..., ReturnIsAsync = true, ...)]`. Initial value is
    /// always overridden by the value comes from reflection.
    ///
    /// Use conventions to set a custom value.
    /// </summary>
    public bool ReturnIsAsync { get; set; } = default!;

    /// <summary>
    /// Do NOT set this property directly from the attribute definition, e.g.,
    /// `[ActionModel(..., ReturnIsVoid = true, ...)]`. Initial value is always
    /// overridden by the value comes from reflection.
    ///
    /// Use conventions to set a custom value.
    /// </summary>
    public bool ReturnIsVoid { get; set; } = default!;

    public Func<string, string> ReturnResultRenderer { get; set; } = resultExpression => resultExpression;
    public string FindTargetStatement { get; set; } = ParameterModelAttribute.TargetParameterName;
    public bool UseForm { get; set; } = false;
    public bool UseRequestClassForBody { get; set; } = true;
    public int Order { get; set; } = 0;
    public List<string> AdditionalAttributes { get; } = [.. additionalAttributes ?? []];
    public List<string> PreparationStatements { get; } = [.. preparationStatements ?? []];
    public Dictionary<string, ParameterModelAttribute> Parameter { get; private set; } = default!;
    public bool Orphan { get; } = false;
    internal bool Initialized { get; private set; } = false;

    public bool HasBody => !UseForm && BodyParameters.Any();
    public IEnumerable<ParameterModelAttribute> Parameters => Parameter.Values;
    IEnumerable<ParameterModelAttribute> ActionParameters => Parameters.Where(p => !p.IsHardCoded).OrderBy(p => p.Order).ThenBy(p => p.IsOptional ? 1 : -1);
    IEnumerable<ParameterModelAttribute> RouteParameters => Parameters.Where(p => p.From == ParameterModelFrom.Route).OrderBy(p => p.RoutePosition);
    IEnumerable<ParameterModelAttribute> NonServiceParameters => ActionParameters.Where(p => p.From != ParameterModelFrom.Services);
    public IEnumerable<ParameterModelAttribute> BodyParameters => ActionParameters.Where(p => p.From == ParameterModelFrom.BodyOrForm);
    public IEnumerable<ParameterModelAttribute> ServiceParameters => ActionParameters.Where(p => p.From == ParameterModelFrom.Services);
    public IEnumerable<ParameterModelAttribute> NonBodyParameters => NonServiceParameters.Where(p => p.From != ParameterModelFrom.BodyOrForm);
    public IEnumerable<ParameterModelAttribute> InvokedMethodParameters => Parameters.Where(p => p.IsInvokeMethodParameter);

    internal ActionModelAttribute Init(string id, IEnumerable<string> routeParts, string returnType, bool returnIsAsync, bool returnIsVoid, IEnumerable<ParameterModelAttribute> parameters)
    {
        if (Initialized) { throw new($"Cannot initialize, already initialized: {Id}"); }

        Id = id;
        Name ??= id;
        RouteParts = RouteParts.Any() ? RouteParts : [.. routeParts];
        ReturnType ??= returnType;
        ReturnIsAsync = returnIsAsync;
        ReturnIsVoid = returnIsVoid;
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