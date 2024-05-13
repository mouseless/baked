using Do.Test.Orm;
using Microsoft.Extensions.Logging;

namespace Do.Test.Business;

public class MethodSamples(ILogger<MethodSamples> _logger)
{
    readonly List<string> _strings = [];

    public void Execute() =>
        _logger.LogInformation($"{nameof(Execute)} was called");

    public void Update() =>
        _logger.LogInformation($"{nameof(Update)} was called");

    public void UpdateString() =>
        _logger.LogInformation($"{nameof(UpdateString)} was called");

    public void Delete() =>
        _logger.LogInformation($"{nameof(Delete)} was called");

    public void Void() =>
        _logger.LogInformation($"{nameof(Void)} was called");

    public List<string> GetStrings() =>
        _strings;

    public void AddSetting(string value) =>
        _strings.Add(value);

    public void AddString(string @string) =>
        _strings.Add(@string);

    public async Task VoidAsync()
    {
        await Task.Delay(10);

        _logger.LogInformation($"{nameof(VoidAsync)} was called");
    }

    public object Object(object @object) =>
        @object;

    public async Task<object> ObjectAsync(object @object)
    {
        await Task.Delay(10);

        return @object;
    }

    public void PrimitiveParameters(string @string, int @int, DateTime dateTime) =>
        _logger.LogInformation($"{nameof(PrimitiveParameters)} was called with {@string}, {@int} and {dateTime}");

    public void PrimitiveListParameters(List<string> strings, int[] ints, IEnumerable<DateTime> dateTimes) =>
        _logger.LogInformation($"{nameof(PrimitiveListParameters)} was called with [{strings.Join(", ")}], [{ints.Join(", ")}] and [{dateTimes.Join(", ")}]");

    public Entity EntityParameters(Entity entity) =>
        entity;

    public IEnumerable<Entity> EntityListParameters(IEnumerable<Entity> entities, Entity[] otherEntities) =>
        [.. entities, .. otherEntities];

    internal Internal Internal() =>
        new();
}