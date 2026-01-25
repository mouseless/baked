namespace Baked.Business;

public interface ILocator<T>
{
    T SingleById(object id, bool throwNotFound);

    IEnumerable<T> ByIds(object id);
}