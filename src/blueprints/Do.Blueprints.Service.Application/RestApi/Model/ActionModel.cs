namespace Do.RestApi.Model;

public record ActionModel(
    string Name,
    HttpMethod Method,
    string Route,
    ReturnModel Return,
    ActionStatementsModel Statements
)
{
    public string Name { get; set; } = Name;
    public HttpMethod Method { get; set; } = Method;
    public string Route { get; set; } = Route;
    public ReturnModel Return { get; set; } = Return;
    public ActionStatementsModel Statements { get; set; } = Statements;

    public List<ParameterModel> Parameters { get; set; } = [];
}
