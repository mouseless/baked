namespace Baked.Business;

[AttributeUsage(AttributeTargets.Class)]
public class LocatableAttribute : Attribute, IMetadataSerializer
{
    public Type? QueryType { get; set; }
    public bool IsAsync { get; set; } = false;

    IEnumerable<MetadataProperty> IMetadataSerializer.Properties =>
        [
            new(nameof(QueryType), QueryType),
            new(nameof(IsAsync), IsAsync)
        ];

    public string RenderLocate(string serviceExpression, string idExpression,
        string throwNotFoundExpression = "false"
    ) => IsAsync
        ? $"await {serviceExpression}.LocateAsync({idExpression}, throwNotFound: {throwNotFoundExpression})"
        : $"{serviceExpression}.Locate({idExpression}, throwNotFound: {throwNotFoundExpression})";

    public string RenderLocateMany(string serviceExpression, string idsExpression) =>
        IsAsync
            ? $"await {serviceExpression}.LocateManyAsync({idsExpression})"
            : $"{serviceExpression}.LocateMany({idsExpression})";

    public string RenderLocatorType(string locatableType) =>
        IsAsync ? $"Baked.Business.IAsyncLocator<{locatableType}>" : $"Baked.Business.ILocator<{locatableType}>";
}