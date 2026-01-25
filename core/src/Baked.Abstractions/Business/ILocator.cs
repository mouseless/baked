namespace Baked.Business;

public interface ILocator<T>
{
    T Single(Id id, bool throwNotFound = false);

    IEnumerable<T> Multiple(IEnumerable<Id> ids);
}