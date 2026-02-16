namespace Baked.Business;

public interface ILocator<T>
{
    T Locate(Id id, bool throwNotFound = false);
    LazyLocatable<T> LocateLazily(Id id);
    IEnumerable<T> LocateMany(IEnumerable<Id> ids);
}