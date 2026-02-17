using Baked.Playground.CodingStyle.RichTransient;
using Baked.Playground.CodingStyle.ValueType;
using Baked.Playground.Orm;
using Microsoft.Extensions.Logging;

namespace Baked.Playground.Business;

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

    public async Task<string> GetAsync(int ms)
    {
        await Task.Delay(ms);

        return "this is from server";
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

    public IEnumerable<Value> GetValueTypeParameters(Value single, IEnumerable<Value> enumerable, Value[] array) =>
        [single, .. enumerable, .. array];

    // TODO fix nullabel array rendering
    // public IEnumerable<Value?> GetValueTypeParametersNullable(Value? single, IEnumerable<Value?> enumerable, Value?[] array) =>
    // [single, .. enumerable, .. array];

    public IEnumerable<Value> ValueTypeParameters(Value single, IEnumerable<Value> enumerable, Value[] array) =>
        [single, .. enumerable, .. array];

    // TODO fix nullabel array rendering
    // public IEnumerable<Value?> ValueTypeParametersNullable(Value? single, IEnumerable<Value?> enumerable, Value?[] array) =>
    // [single, .. enumerable, .. array];

    public IEnumerable<Value> RecordWithValueType(RecordWith<Value> record) =>
        [record.Single, .. record.Enumerable, .. record.Array];

    public IEnumerable<Value?> RecordWithValueTypeNullable(RecordWith<Value?> record) =>
        [record.Single, .. record.Enumerable, .. record.Array];

    /// <param name="single">
    /// Single description
    /// </param>
    /// <param name="enumerable">
    /// Enumerable description
    /// </param>
    /// <param name="array">
    /// Array description
    /// </param>
    public IEnumerable<Entity> GetEntityParameters(Entity single, IEnumerable<Entity> enumerable, Entity[] array) =>
        [single, .. enumerable, .. array];

    public IEnumerable<Entity> EntityParameters(Entity single, IEnumerable<Entity> enumerable, Entity[] array) =>
        [single, .. enumerable, .. array];

    public IEnumerable<Entity> RecordWithEntity(RecordWith<Entity> record) =>
        [record.Single, .. record.Enumerable, .. record.Array];

    /// <param name="single">
    /// Single description
    /// </param>
    /// <param name="enumerable">
    /// Enumerable description
    /// </param>
    /// <param name="array">
    /// Array description
    /// </param>
    public IEnumerable<RichTransientWithData> GetTransientParameters(RichTransientWithData single, IEnumerable<RichTransientWithData> enumerable, RichTransientWithData[] array) =>
        [single, .. enumerable, .. array];

    public IEnumerable<RichTransientWithData> TransientParameters(RichTransientWithData single, IEnumerable<RichTransientWithData> enumerable, RichTransientWithData[] array) =>
        [single, .. enumerable, .. array];

    public IEnumerable<RichTransientWithData> RecordWithRichTransient(RecordWith<RichTransientWithData> record) =>
        [record.Single, .. record.Enumerable, .. record.Array];

    /// <param name="single">
    /// Single description
    /// </param>
    /// <param name="enumerable">
    /// Enumerable description
    /// </param>
    /// <param name="array">
    /// Array description
    /// </param>
    public IEnumerable<RichTransientAsync> GetTransientAsyncParameters(RichTransientAsync single, IEnumerable<RichTransientAsync> enumerable, RichTransientAsync[] array) =>
        [single, .. enumerable, .. array];

    public IEnumerable<RichTransientAsync> TransientAsyncParameters(RichTransientAsync single, IEnumerable<RichTransientAsync> enumerable, RichTransientAsync[] array) =>
        [single, .. enumerable, .. array];

    public IEnumerable<RichTransientAsync> RecordWithRichTransientAsync(RecordWith<RichTransientAsync> record) =>
        [record.Single, .. record.Enumerable, .. record.Array];

    internal Internal Internal() =>
        new();
}