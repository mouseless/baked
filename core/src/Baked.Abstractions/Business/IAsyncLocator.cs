namespace Baked.Business;

public interface IAsyncLocator<T> : ILocator<T> where T : class
{
    Task<T> LocateAsync(Id id, bool throwNotFound = false);
    Task<IEnumerable<T>> LocateManyAsync(IEnumerable<Id> ids);

    public async Task<T?> LocateNullableAsync(string? id) =>
        id is not null ? await LocateAsync(id) : null;
}