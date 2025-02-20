namespace Baked.RestApi;

[AttributeUsage(AttributeTargets.Method)]
public class ActionModelAttribute(string id, ActionModelMethod method, string[] routeParts, string findTargetStatement,
    string[]? additionalAttributes = default,
    string[]? preparationStatements = default
) : Attribute
{
    public string Id { get; } = id;
    public string? Name { get; set; }
    public ActionModelMethod Method { get; set; } = method;
    public List<string> RouteParts { get; set; } = [.. routeParts ?? []];
    public string? ReturnType { get; set; }
    public bool? ReturnIsAsync { get; set; }
    public bool? ReturnIsVoid { get; set; }
    public Func<string, string>? ReturnResultRenderer { get; set; }
    public string FindTargetStatement { get; set; } = findTargetStatement;
    public bool? UseForm { get; set; }
    public bool? UseRequestClassForBody { get; set; }
    public int? Order { get; set; }
    public List<string> AdditionalAttributes { get; set; } = [.. additionalAttributes ?? []];
    public List<string> PreparationStatements { get; set; } = [.. preparationStatements ?? []];
}