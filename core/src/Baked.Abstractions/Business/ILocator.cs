namespace Baked.Business;

public interface ILocator<T>
{
    T Locate(Id id, bool throwNotFound = false);
    IEnumerable<T> LocateMany(IEnumerable<Id> ids);
}