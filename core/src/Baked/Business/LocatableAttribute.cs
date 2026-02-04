namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class)]
public class LocatableAttribute : Attribute
{
    public bool IsAsync { get; set; } = false;

    public Locate LocateRenderer => (serviceExpression, idExpression, throwNotFoundExpression) => IsAsync
        ? $"await {serviceExpression}.LocateAsync({idExpression}, throwNotFound: {throwNotFoundExpression})"
        : $"{serviceExpression}.Locate({idExpression}, throwNotFound: {throwNotFoundExpression})";
    public LocateMany LocateManyRenderer => (serviceExpression, idsExpression) => IsAsync
        ? $"await {serviceExpression}.LocateManyAsync({idsExpression})"
        : $"{serviceExpression}.LocateMany({idsExpression})";

    public delegate string Locate(string serviceExpression, string idExpression, string throwNotFoundExpression = "false");
    public delegate string LocateMany(string serviceExpression, string idsExpression);

    public string BuildLocatorType(string locatableType) =>
        IsAsync ? $"Baked.Business.IAsyncLocator<{locatableType}>" : $"Baked.Business.ILocator<{locatableType}>";
}