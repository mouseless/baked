namespace Baked.Binding.Rest;

public class SwaggerSchemaHelper
{
    readonly Dictionary<string, List<string>> _schemaNameRepetition = [];

    public string GetSchemaId(Type modelType)
    {
        var id = DefaultSchemaIdSelector(modelType);

        if (!_schemaNameRepetition.ContainsKey(id))
        {
            _schemaNameRepetition.Add(id, new List<string>());
        }

        var modelNameList = _schemaNameRepetition[id];
        var fullName = modelType.FullName ?? string.Empty;
        if (!string.IsNullOrEmpty(fullName) && !modelNameList.Contains(fullName))
        {
            modelNameList.Add(fullName);
        }

        var index = modelNameList.IndexOf(fullName);

        return $"{id}{(index >= 1 ? index.ToString() : string.Empty)}";
    }

    string DefaultSchemaIdSelector(Type modelType)
    {
        if (!modelType.IsConstructedGenericType)
        {
            return modelType.Name.Replace("[]", "Array");
        }

        var prefix = modelType.GetGenericArguments()
            .Select(DefaultSchemaIdSelector)
            .Aggregate((previous, current) => previous + current);

        return prefix + modelType.Name.Split('`').First();
    }
}