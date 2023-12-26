namespace Domain.Business;

public interface IQueryContext<T> where T : class
{
    List<T> All();
}
