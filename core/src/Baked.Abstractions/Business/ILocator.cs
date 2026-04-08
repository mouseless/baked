namespace Baked.Business;

public interface ILocator<T> where T : class
{
    T Locate(Id id, bool throwNotFound = false);
    LazyLocatable<T> LocateLazily(Id id);
    IEnumerable<T> LocateMany(IEnumerable<Id> ids);

    public T? LocateNullable(string? id) =>
        id is not null ? Locate(id) : null;
}