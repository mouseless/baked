using System.Linq.Expressions;

namespace Do.Orm;

public interface IQueryContext<TEntity>
{
    IQueryable<TEntity> Query();

    public TEntity? SingleBy(Expression<Func<TEntity, bool>> whereClause) =>
        Query().Where(whereClause).SingleOrDefault();

    public TEntity? FirstBy<TOrderBy>(Expression<Func<TEntity, bool>> whereClause,
        Expression<Func<TEntity, TOrderBy>>? orderByDescending = default
    )
    {
        var query = Query().Where(whereClause);

        if (orderByDescending is not null)
        {
            query = query.OrderByDescending(orderByDescending);
        }

        return query.FirstOrDefault();
    }
}
