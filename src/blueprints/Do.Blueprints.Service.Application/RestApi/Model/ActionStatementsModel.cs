namespace Do.RestApi.Model;

public record ActionStatementsModel(string FindTarget, InvokeMethodModel InvokeMethod)
{
    public string FindTarget { get; set; } = FindTarget;
    public InvokeMethodModel InvokeMethod { get; set; } = InvokeMethod;
}
