using System.Linq.Expressions;

namespace Baked.Orm;

public interface IQueryContext<TEntity>
{
    TEntity SingleById(Guid id, bool throwNotFound = false);
    IQueryable<TEntity> Query();

    public List<TEntity> ByIds(IEnumerable<Guid> ids) =>
        ids.Select(id => SingleById(id)).ToList();

    public bool AnyBy(
        Expression<Func<TEntity, bool>>? where = null,
        List<(bool, Expression<Func<TEntity, bool>>)>? whereIf = null
    ) => Query(where, whereIf: whereIf).Any();

    public TEntity? SingleBy(
        Expression<Func<TEntity, bool>>? where = null,
        List<(bool, Expression<Func<TEntity, bool>>)>? whereIf = null
    ) => Query(where, whereIf: whereIf).SingleOrDefault();

    public TEntity? FirstBy(
        Expression<Func<TEntity, bool>>? where = null,
        List<(bool, Expression<Func<TEntity, bool>>)>? whereIf = null
    ) => Query(where, whereIf: whereIf).FirstOrDefault();

    public TEntity? FirstBy<TOrderBy>(
        Expression<Func<TEntity, bool>>? where = null,
        List<(bool, Expression<Func<TEntity, bool>>)>? whereIf = null,
        Expression<Func<TEntity, TOrderBy>>? orderBy = default,
        Expression<Func<TEntity, TOrderBy>>? orderByDescending = default
    ) => Query(where,
            whereIf: whereIf,
            orderBy: orderBy,
            orderByDescending: orderByDescending
         ).FirstOrDefault();

    public List<TEntity> By(
        Expression<Func<TEntity, bool>>? where = null,
        List<(bool, Expression<Func<TEntity, bool>>)>? whereIf = null,
        int? take = null,
        int? skip = null
    )
    {
        var result = Query(where,
            whereIf: whereIf,
            take: take,
            skip: skip
        );

        return [.. result];
    }

    public List<TEntity> By<TOrderBy>(
        Expression<Func<TEntity, bool>>? where = null,
        List<(bool, Expression<Func<TEntity, bool>>)>? whereIf = null,
        Expression<Func<TEntity, TOrderBy>>? orderBy = default,
        Expression<Func<TEntity, TOrderBy>>? orderByDescending = default,
        int? take = null,
        int? skip = null
    )
    {
        var result = Query(where,
            whereIf: whereIf,
            orderBy: orderBy,
            orderByDescending: orderByDescending,
            take: take,
            skip: skip
         );

        return [.. result];
    }

    IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? where,
        List<(bool, Expression<Func<TEntity, bool>>)>? whereIf = null,
        int? take = null,
        int? skip = null
    )
    {
        var query = Query();

        if (where is not null)
        {
            query = query.Where(where);
        }

        if (whereIf is not null)
        {
            foreach (var (condition, clause) in whereIf)
            {
                if (!condition) { continue; }

                query = query.Where(clause);
            }
        }

        if (take is not null)
        {
            query = query.Take(take.Value);
        }

        if (skip is not null)
        {
            query = query.Skip(skip.Value);
        }

        return query;
    }

    IQueryable<TEntity> Query<TOrderBy>(Expression<Func<TEntity, bool>>? where,
        List<(bool, Expression<Func<TEntity, bool>>)>? whereIf = null,
        Expression<Func<TEntity, TOrderBy>>? orderBy = default,
        Expression<Func<TEntity, TOrderBy>>? orderByDescending = default,
        int? take = null,
        int? skip = null
    )
    {
        var query = Query(where,
            whereIf: whereIf
        );

        if (orderBy is not null)
        {
            query = query.OrderBy(orderBy);
        }

        if (orderByDescending is not null)
        {
            query = query.OrderByDescending(orderByDescending);
        }

        if (take is not null)
        {
            query = query.Take(take.Value);
        }

        if (skip is not null)
        {
            query = query.Skip(skip.Value);
        }

        return query;
    }
}