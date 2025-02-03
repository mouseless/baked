namespace Baked.Business.DomainAssemblies;

public class SwaggerSchemaHelper
{
    private readonly Dictionary<string, List<string>> _schemaNameRepetition = new();

    private string DefaultSchemaIdSelector(Type modelType)
    {
        if (!modelType.IsConstructedGenericType) return modelType.Name.Replace("[]", "Array");

        var prefix = modelType.GetGenericArguments()
            .Select(genericArg => DefaultSchemaIdSelector(genericArg))
            .Aggregate((previous, current) => previous + current);

        return prefix + modelType.Name.Split('`').First();
    }

    public string GetSchemaId(Type modelType)
    {
        string id = DefaultSchemaIdSelector(modelType);

        if (!_schemaNameRepetition.ContainsKey(id))
            _schemaNameRepetition.Add(id, new List<string>());

        var modelNameList = _schemaNameRepetition[id];
        var fullName = modelType.FullName ?? string.Empty;
        if (!string.IsNullOrEmpty(fullName) && !modelNameList.Contains(fullName))
            modelNameList.Add(fullName);

        int index = modelNameList.IndexOf(fullName);

        return $"{id}{(index >= 1 ? index.ToString() : string.Empty)}";
    }
}