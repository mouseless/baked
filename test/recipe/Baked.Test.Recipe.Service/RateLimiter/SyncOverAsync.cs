using Baked.Communication;
using Baked.Test.Orm;

namespace Baked.Test.RateLimiter;

public class SyncOverAsync(
    Entities _entities,
    Func<Entity> _newEntity,
    IClient<SyncOverAsync> _client
)
{
    public async Task<object> Execute()
    {
        var entities = _entities.By();
        var response = await _client.Send(new("/random-names", HttpMethod.Get));
        _newEntity().With(int32: entities.Count);

        return response;
    }
}