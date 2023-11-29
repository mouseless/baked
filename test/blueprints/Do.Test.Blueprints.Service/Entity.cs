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
    public virtual Status Enum { get; protected set; } = default!;
    public virtual DateTime DateTime { get; protected set; } = default!;

    public virtual Entity With(
        Guid? guid = default,
        string? @string = default,
        string? stringData = default,
        int? int32 = default,
        Uri? uri = default,
        object? @dynamic = default,
        Status? @enum = default,
        DateTime? dateTime = default
    )
    {
        Set(
            guid: guid,
            @string: @string,
            stringData: stringData,
            int32: int32,
            uri: uri,
            @dynamic: @dynamic,
            @enum: @enum,
            dateTime: dateTime
        );

        return _context.Insert(this);
    }

    public virtual async Task Update(
        Guid? guid = default,
        string? @string = default,
        string? stringData = default,
        int? int32 = default,
        Uri? uri = default,
        object? @dynamic = default,
        Status? @enum = default,
        DateTime? dateTime = default,
        bool useTransaction = false,
        bool throwError = false
    )
    {
        if (useTransaction)
        {
            await _transaction.CommitAsync(this, @this =>
                @this.Set(
                    guid: guid,
                    @string: @string,
                    stringData: stringData,
                    int32: int32,
                    uri: uri,
                    @dynamic: @dynamic,
                    @enum: @enum,
                    dateTime: dateTime
               )
            );
        }
        else
        {
            Set(
                guid: guid,
                @string: @string,
                stringData: stringData,
                int32: int32,
                uri: uri,
                @dynamic: @dynamic,
                @enum: @enum,
                dateTime: dateTime
            );
        }

        if (throwError)
        {
            throw new();
        }
    }

    protected virtual void Set(
        Guid? guid = default,
        string? @string = default,
        string? stringData = default,
        int? int32 = default,
        Uri? uri = default,
        object? @dynamic = default,
        Status? @enum = default,
        DateTime? dateTime = default
    )
    {
        Guid = guid ?? Guid;
        String = @string ?? String;
        StringData = stringData ?? StringData;
        Int32 = int32 ?? Int32;
        Uri = uri ?? Uri;
        Dynamic = @dynamic ?? Dynamic;
        Enum = @enum ?? Enum;
        DateTime = dateTime ?? DateTime;
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

    public List<Entity> By(
        Guid? guid = default,
        string? @string = default,
        string? stringData = default,
        int? int32 = default,
        Uri? uri = default,
        Status? status = default,
        DateTime? dateTime = default,
        int? take = default,
        int? skip = default
    )
    {
        return _context.By(
            where: e =>
                (guid == default || e.Guid == guid) &&
                (@string == default || e.String == @string) &&
                (stringData == default || e.StringData == @stringData) &&
                (int32 == default || e.Int32 == int32) &&
                (uri == default || e.Uri == uri) &&
                (status == default || e.Enum == status) &&
                (dateTime == default || e.DateTime == dateTime),
            take: take,
            skip: skip
        );
    }
}
