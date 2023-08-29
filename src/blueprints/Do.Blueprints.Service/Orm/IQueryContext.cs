using System.Linq.Expressions;

namespace Do.Orm;

public interface IQueryContext<TEntity>
{
    TEntity SingleById(Guid id);
    IQueryable<TEntity> Query();

    public TEntity? SingleBy(Expression<Func<TEntity, bool>> where) =>
        Query(where).SingleOrDefault();

    public TEntity? FirstBy(Expression<Func<TEntity, bool>> where) =>
        Query(where).FirstOrDefault();

    public TEntity? FirstBy<TOrderBy>(
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, TOrderBy>>? orderBy = default,
        Expression<Func<TEntity, TOrderBy>>? orderByDescending = default
    ) => Query(
            where,
            orderBy: orderBy,
            orderByDescending: orderByDescending
         ).FirstOrDefault();

    public List<TEntity> By(Expression<Func<TEntity, bool>> where) =>
        Query(where).ToList();

    public List<TEntity> By<TOrderBy>(
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, TOrderBy>>? orderBy = default,
        Expression<Func<TEntity, TOrderBy>>? orderByDescending = default
    ) => Query(
            where,
            orderBy: orderBy,
            orderByDescending: orderByDescending
         ).ToList();

    public List<TEntity> All() => Query().ToList();

    public List<TEntity> All<TOrderBy>(
        Expression<Func<TEntity, TOrderBy>>? orderBy = default,
        Expression<Func<TEntity, TOrderBy>>? orderByDescending = default
    ) => Query(
            t => true,
            orderBy: orderBy,
            orderByDescending: orderByDescending
         ).ToList();

    IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where) =>
        Query().Where(where);

    IQueryable<TEntity> Query<TOrderBy>(
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, TOrderBy>>? orderBy = default,
        Expression<Func<TEntity, TOrderBy>>? orderByDescending = default
    )
    {
        var query = Query().Where(where);

        if (orderBy is not null)
        {
            query = query.OrderBy(orderBy);
        }

        if (orderByDescending is not null)
        {
            query = query.OrderByDescending(orderByDescending);
        }

        return query;
    }
}
