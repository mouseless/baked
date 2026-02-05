namespace Baked.Business;

public interface IAsyncLocator<T> : ILocator<T>
{
    Task<T> LocateAsync(Id id, bool throwNotFound = false);
    Task<IEnumerable<T>> LocateManyAsync(IEnumerable<Id> ids);
}