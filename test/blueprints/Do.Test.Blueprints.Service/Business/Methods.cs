using Do.Test.Orm;
using Microsoft.Extensions.Logging;

namespace Do.Test.Business;

public class Methods(
    ILogger<Methods> _logger
)
{
    [AuthorizationRequired]
    public void RequiresAuthorization() =>
        _logger.LogInformation($"{nameof(RequiresAuthorization)} was called");

    internal Internal Internal() =>
        new();

    public void Void() =>
        _logger.LogInformation($"{nameof(Void)} was called");

    public async Task VoidAsync()
    {
        await Task.Delay(10);

        _logger.LogInformation($"{nameof(VoidAsync)} was called");
    }

    public void PrimitiveParameters(string @string, int @int, DateTime dateTime) =>
        _logger.LogInformation($"{nameof(PrimitiveParameters)} was called with {@string}, {@int} and {dateTime}");

    public void PrimitiveListParameters(List<string> strings, int[] ints, IEnumerable<DateTime> dateTimes) =>
        _logger.LogInformation($"{nameof(PrimitiveListParameters)} was called with [{strings.Join(", ")}], [{ints.Join(", ")}] and [{dateTimes.Join(", ")}]");

    public void EntityParameters(Entity entity) =>
        _logger.LogInformation($"{nameof(EntityParameters)} was called with {entity.Id}");

    public void EntityListParameters(IEnumerable<Entity> entities, Entity[] otherEntities) =>
        _logger.LogInformation($"{nameof(EntityListParameters)} was called with [{entities.Select(e => e.Id).Join(", ")}] and [{otherEntities.Select(e => e.Id).Join(", ")}]");

    public object Object(object @object) =>
        @object;

    public async Task<object> ObjectAsync(object @object)
    {
        await Task.Delay(10);

        return @object;
    }
}