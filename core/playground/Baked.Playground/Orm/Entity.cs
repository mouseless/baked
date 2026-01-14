using Baked.Database;
using Baked.Orm;

namespace Baked.Playground.Orm;

/// <summary>
/// Sample entity
/// </summary>
/// <remarks>
/// It is a test entity to check all supported property types both in data
/// access layer and in rest api layer.
/// </remarks>
public class Entity(IEntityContext<Entity> _context, Entities _entities, ITransaction _transaction)
{
    public Id Id { get; private set; } = default!;
    public Guid? Guid { get; private set; } = default!;
    public string? String { get; private set; } = default!;
    /// <summary>
    /// Data suffix should cause this property to map to a TEXT column in MySql
    /// etc.
    /// </summary>
    public string? StringData { get; private set; } = default!;
    public int? Int32 { get; private set; } = default!;
    public string? Unique { get; private set; } = default!;
    public Uri? Uri { get; private set; } = default!;
    /// <summary>
    /// Object type properties are converted to json strings in db, dynamic
    /// json objects in rest api layer.
    /// </summary>
    public object? Dynamic { get; private set; } = default!;
    public Enumeration? Enum { get; private set; } = default!;
    public DateTime? DateTime { get; private set; } = default!;

    public Entity With(
        Guid? guid = default,
        string? @string = default,
        string? stringData = default,
        int? int32 = default,
        string? unique = default,
        Uri? uri = default,
        object? @dynamic = default,
        Enumeration? @enum = default,
        DateTime? dateTime = default
    )
    {
        Set(
            guid: guid,
            @string: @string,
            stringData: stringData,
            int32: int32,
            unique: unique,
            uri: uri,
            @dynamic: @dynamic,
            @enum: @enum,
            dateTime: dateTime
        );

        return _context.Insert(this);
    }

    public async Task Update(
        Guid? guid = default,
        string? @string = default,
        string? stringData = default,
        int? int32 = default,
        string? unique = default,
        Uri? uri = default,
        object? @dynamic = default,
        Enumeration? @enum = default,
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
                    unique: unique,
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
                unique: unique,
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

    public void UpdateString(
        string? @string = default
    ) => Set(@string: @string);

    public async Task<int> LockAndIncrementInt32(
        int offset = 1,
        int delayInSeconds = 5
    )
    {
        await Task.Delay(TimeSpan.FromSeconds(delayInSeconds));

        _context.Lock(this);

        Set(int32: Int32 + offset);

        return Int32 ?? 0;
    }

    void Set(
        Guid? guid = default,
        string? @string = default,
        string? stringData = default,
        int? int32 = default,
        string? unique = default,
        Uri? uri = default,
        object? @dynamic = default,
        Enumeration? @enum = default,
        DateTime? dateTime = default
    )
    {
        if (unique is not null && unique != Unique && _entities.AnyByUnique(unique))
        {
            throw new MustBeUniqueException(nameof(Unique));
        }

        if (@enum is not null && @enum != Enum && _entities.AnyByEnum(@enum.Value))
        {
            throw new MustBeUniqueException(nameof(Enum));
        }

        Guid = guid ?? Guid;
        String = @string ?? String;
        StringData = stringData ?? StringData;
        Int32 = int32 ?? Int32;
        Unique = unique ?? Unique;
        Uri = uri ?? Uri;
        Dynamic = @dynamic ?? Dynamic;
        Enum = @enum ?? Enum;
        DateTime = dateTime ?? DateTime;
    }

    public void Delete()
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
        string? unique = default,
        Uri? uri = default,
        Enumeration? @enum = default,
        DateTime? dateTime = default,
        int? take = default,
        int? skip = default
    ) => _context.By(
            whereIf:
            [
                (guid is not null, e => e.Guid == guid),
                (@string is not null, e => e.String == @string),
                (stringData is not null, e => e.StringData == @stringData),
                (int32 is not null, e => e.Int32 == int32),
                (unique is not null, e => e.Unique == unique),
                (uri is not null, e => e.Uri == uri),
                (@enum is not null, e => e.Enum == @enum),
                (dateTime is not null, e => e.DateTime == dateTime),
            ],
            take: take,
            skip: skip
        );

    internal bool AnyByUnique(string unique) =>
        _context.AnyBy(e => e.Unique == unique);

    internal bool AnyByEnum(Enumeration @enum) =>
        _context.AnyBy(e => e.Enum == @enum);

    public Entity SingleByUnique(string unique,
        bool throwNotFound = false
    ) => _context.SingleBy(e => e.Unique == unique) ?? throw RecordNotFoundException.For<Entity>(nameof(unique), unique, notFound: throwNotFound);

    public Entity SingleByEnum(Enumeration @enum,
        bool throwNotFound = false
    ) => _context.SingleBy(e => e.Enum == @enum) ?? throw RecordNotFoundException.For<Entity>(nameof(@enum), @enum, notFound: throwNotFound);

    public Entity? FirstByString(string startsWith,
        bool asc = false,
        bool desc = false
    ) => asc ? _context.FirstBy(e => e.String != null && e.String.StartsWith(startsWith), orderBy: e => e.String) :
         desc ? _context.FirstBy(e => e.String != null && e.String.StartsWith(startsWith), orderByDescending: e => e.String) :
         _context.FirstBy(e => e.String != null && e.String.StartsWith(startsWith));
}