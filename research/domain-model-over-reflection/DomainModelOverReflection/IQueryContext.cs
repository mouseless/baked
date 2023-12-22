namespace DomainModelOverReflection;

public interface IQueryContext<T> where T : class
{
    List<T> All();
}
