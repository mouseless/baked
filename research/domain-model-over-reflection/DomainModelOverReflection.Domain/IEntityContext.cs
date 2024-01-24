namespace DomainModelOverReflection.Domain.Business;

public interface IEntityContext<T> where T : class
{
    T Insert(T instance);
}
