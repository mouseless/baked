using Do.Database;
using Do.Orm;

namespace Do.Test;

public class Entity
{
    readonly IEntityContext<Entity> _context = default!;
    readonly ITransaction _transaction = default!;

    protected Entity() { }
    public Entity(IEntityContext<Entity> context, ITransaction transaction) =>
        (_context, _transaction) = (context, transaction);

    public virtual Guid Id { get; protected set; } = default!;
    public virtual Guid Guid { get; protected set; } = default!;
    public virtual string String { get; protected set; } = default!;
    public virtual string StringData { get; protected set; } = default!;
    public virtual int Int32 { get; protected set; } = default!;
    public virtual Uri Uri { get; protected set; } = default!;
    public virtual object Dynamic { get; protected set; } = default!;

    public virtual Entity With(Guid guid, string @string, string stringData, int int32, Uri uri, object @dynamic)
    {
        Set(guid, @string, stringData, int32, uri, @dynamic);

        return _context.Insert(this);
    }

    public virtual async Task Update(Guid guid, string @string, string stringData, int int32, Uri uri, object @dynamic,
        bool useTransaction = false,
        bool throwError = false
    )
    {
        if (useTransaction) { await _transaction.CommitAsync(this, @this => @this.Set(guid, @string, stringData, int32, uri, @dynamic)); }
        else { Set(guid, @string, stringData, int32, uri, @dynamic); }

        if (throwError) { throw new Exception(); }
    }

    protected virtual void Set(Guid guid, string @string, string stringData, int int32, Uri uri, object @dynamic)
    {
        Guid = guid;
        String = @string;
        StringData = stringData;
        Int32 = int32;
        Uri = uri;
        Dynamic = @dynamic;
    }

    public virtual void Delete()
    {
        _context.Delete(this);
    }
}

public class Entities
{
    readonly IQueryContext<Entity> _context;

    public Entities(IQueryContext<Entity> context) =>
        _context = context;

    public List<Entity> By(string? @string = default)
    {
        if (@string == default) { return _context.All(); }

        return _context.By(e => e.String == @string);
    }
}
