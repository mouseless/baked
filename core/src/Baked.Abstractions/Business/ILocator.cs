namespace Baked.Business;

public interface ILocator<T>
{
    T Locate(Id id, bool throwNotFound = false);
    (T, Task) LocateLazily(Id id);
    IEnumerable<T> LocateMany(IEnumerable<Id> ids);
}