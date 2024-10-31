using Baked.Test.CodingStyle.RichTransient;
using Baked.Test.Orm;
using Microsoft.Extensions.Logging;

namespace Baked.Test.Business;

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

    public void SetSetting(string value) =>
        _strings.Add(value);

    public void AddString(string @string) =>
        _strings.Add(@string);

    public async Task VoidAsync()
    {
        await Task.Delay(10);

        _logger.LogInformation($"{nameof(VoidAsync)} was called");
    }

    public Record RequestClass(string text, int numeric) =>
         new(text, numeric);

    /// <example for="rest-api">
    /// <code for="request">
    /// {
    ///   "any": "object"
    /// }
    /// </code>
    /// <code for="response">
    /// {
    ///   "will": "do"
    /// }
    /// </code>
    /// </example>
    public object Object(object @object) =>
        @object;

    public async Task<object> ObjectAsync(object @object)
    {
        await Task.Delay(10);

        return @object;
    }

    public object MultipleObjects(object object1, object object2) =>
        new { object1, object2 };

    public void PrimitiveParameters(string @string, int @int, DateTime dateTime) =>
        _logger.LogInformation($"{nameof(PrimitiveParameters)} was called with {@string}, {@int} and {dateTime}");

    public void PrimitiveListParameters(List<string> strings, int[] ints, IEnumerable<DateTime> dateTimes) =>
        _logger.LogInformation($"{nameof(PrimitiveListParameters)} was called with [{strings.Join(", ")}], [{ints.Join(", ")}] and [{dateTimes.Join(", ")}]");

    public void RecordParameters(Record record) =>
        _logger.LogInformation($"{nameof(RecordParameters)} was called with {record}");

    public void RecordListParameters(List<Record> records) =>
        _logger.LogInformation($"{nameof(RecordParameters)} was called with {records.Join(", ")}");

    /// <param name="entity">
    /// Entity description
    /// </param>
    public Entity EntityParameters(Entity entity) =>
        entity;

    /// <param name="entities">
    /// Entities description
    /// </param>
    /// <param name="otherEntities">
    /// Other entities description
    /// </param>
    public IEnumerable<Entity> EntityListParameters(IEnumerable<Entity> entities, Entity[] otherEntities) =>
        [.. entities, .. otherEntities];

    /// <param name="transient">
    /// Transient description
    /// </param>
    public RichTransientWithData TransientParameters(RichTransientWithData transient) =>
        transient;

    /// <param name="transients">
    /// Transients description
    /// </param>
    /// <param name="otherTransients">
    /// Other transients description
    /// </param>
    public IEnumerable<RichTransientWithData> TransientListParameters(IEnumerable<RichTransientWithData> transients, RichTransientWithData[] otherTransients) =>
        [.. transients, .. otherTransients];

    internal Internal Internal() =>
        new();
}