using Humanizer;

namespace Baked.RestApi.Model;

[AttributeUsage(AttributeTargets.Method)]
public class ActionModel(string method, string[] routeParts, string findTargetStatement,
    string[]? additionalAttributes = default,
    string[]? preparationStatements = default
) : Attribute
{
    public ActionModel(string id, HttpMethod method, IEnumerable<string> routeParts, string findTargetStatement, string returnType, bool returnIsAsync, bool returnIsVoid, IEnumerable<ParameterModel> parameters,
        IEnumerable<string>? additionalAttributes = default,
        IEnumerable<string>? preparationStatements = default
    ) : this(method.ToString(), [.. routeParts], findTargetStatement,
        additionalAttributes: additionalAttributes?.ToArray(),
        preparationStatements: preparationStatements?.ToArray()
    )
    {
        Init(id, returnType, returnIsAsync, returnIsVoid, parameters);

        ManuallyAdded = true;
    }

    public string Id { get; private set; } = default!;
    public string Name { get; set; } = default!;
    public HttpMethod Method { get; set; } = HttpMethod.Parse(method);
    public List<string> RouteParts { get; set; } = [.. routeParts];
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
    public string FindTargetStatement { get; set; } = findTargetStatement;
    public bool UseForm { get; set; } = false;
    public bool UseRequestClassForBody { get; set; } = true;
    public int Order { get; set; } = 0;
    public List<string> AdditionalAttributes { get; } = [.. additionalAttributes ?? []];
    public List<string> PreparationStatements { get; } = [.. preparationStatements ?? []];
    public Dictionary<string, ParameterModel> Parameter { get; private set; } = default!;
    public bool ManuallyAdded { get; } = false;

    public bool HasBody => !UseForm && BodyParameters.Any();
    public IEnumerable<ParameterModel> Parameters => Parameter.Values;
    IEnumerable<ParameterModel> ActionParameters => Parameters.Where(p => !p.IsHardCoded).OrderBy(p => p.Order).ThenBy(p => p.IsOptional ? 1 : -1);
    IEnumerable<ParameterModel> RouteParameters => Parameters.Where(p => p.From == ParameterModelFrom.Route).OrderBy(p => p.RoutePosition);
    IEnumerable<ParameterModel> NonServiceParameters => ActionParameters.Where(p => p.From != ParameterModelFrom.Services);
    public IEnumerable<ParameterModel> BodyParameters => ActionParameters.Where(p => p.From == ParameterModelFrom.BodyOrForm);
    public IEnumerable<ParameterModel> ServiceParameters => ActionParameters.Where(p => p.From == ParameterModelFrom.Services);
    public IEnumerable<ParameterModel> NonBodyParameters => NonServiceParameters.Where(p => p.From != ParameterModelFrom.BodyOrForm);
    public IEnumerable<ParameterModel> InvokedMethodParameters => Parameters.Where(p => p.IsInvokeMethodParameter);

    internal ActionModel Init(string id, string returnType, bool returnIsAsync, bool returnIsVoid, IEnumerable<ParameterModel> parameters)
    {
        Id = id;
        Name ??= id;
        ReturnType ??= returnType;
        ReturnIsAsync = returnIsAsync;
        ReturnIsVoid = returnIsVoid;
        Parameter ??= parameters.ToDictionary(p => p.Id);

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

    public string RenderReturnResult(string resultExpression) =>
        ReturnResultRenderer(resultExpression);
}