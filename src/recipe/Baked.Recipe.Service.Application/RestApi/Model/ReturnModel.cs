using Baked.Domain.Model;

namespace Baked.RestApi.Model;

public record ReturnModel(TypeModel TypeModel)
{
    public string Type { get; set; } = TypeModel.CSharpFriendlyFullName;
    public bool IsAsync { get; set; } = TypeModel.IsAssignableTo<Task>();
    public bool IsVoid { get; set; } = TypeModel.Is(typeof(void)) || TypeModel.Is<Task>();
    public Func<string, string> ResultRenderer { get; set; } = resultExpression => resultExpression;

    public string RenderResult(string resultExpression) =>
        ResultRenderer(resultExpression);
}