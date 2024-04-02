using Microsoft.Extensions.Logging;

namespace Do.Test;

public class Methods(
    ILogger<Singleton> _logger
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
        _logger.LogInformation($"{nameof(PrimitiveListParameters)} was called with [{string.Join(", ", strings)}], [{string.Join(", ", ints)}] and [{string.Join(", ", dateTimes)}]");

    public void EntityParameters(Entity entity, Child child, Parent parent) =>
        _logger.LogInformation($"{nameof(EntityParameters)} was called with {entity.Id}, {child.Id} and {parent.Id}");

    public object Object(object @object) =>
        @object;

    public async Task<object> ObjectAsync(object @object)
    {
        await Task.Delay(10);

        return @object;
    }
}
