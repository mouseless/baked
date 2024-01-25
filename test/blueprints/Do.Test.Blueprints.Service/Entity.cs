using Do.Database;
using Do.Orm;

namespace Do.Test;

public class Entity(IEntityContext<Entity> _context, ITransaction _transaction, TimeProvider _timeProvider)
{
    protected Entity() : this(default!, default!, default!) { }

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
        DateTime? dateTime = default,
        bool? setNowForDateTime = default
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
            dateTime: setNowForDateTime == true ? _timeProvider.GetNow() : dateTime
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

public class Entities(IQueryContext<Entity> _context)
{
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

    public Entity? SingleByString(string @string)
    {
        return _context.SingleBy(e => e.String == @string);
    }

    public Entity? FirstByString(string startsWith,
        bool asc = false,
        bool desc = false
    )
    {
        return asc ? _context.FirstBy(e => e.String.StartsWith(startsWith), orderBy: e => e.String) :
               desc ? _context.FirstBy(e => e.String.StartsWith(startsWith), orderByDescending: e => e.String) :
               _context.FirstBy(e => e.String.StartsWith(startsWith));
    }
}
