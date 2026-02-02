namespace Baked.Business;

public interface ILocator<T>
{
    T Locate(Id id, bool throwNotFound = false);
    IEnumerable<T> LocateMany(IEnumerable<Id> ids);
}

public interface IAsyncLocator<T>
{
    Task<T> LocateAsync(Id id, bool throwNotFound = false);
    Task<IEnumerable<T>> LocateManyAsync(IEnumerable<Id> ids);
}