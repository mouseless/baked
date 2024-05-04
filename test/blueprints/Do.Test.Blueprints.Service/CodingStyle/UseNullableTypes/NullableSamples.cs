using Do.Test.Orm;

namespace Do.Test.CodingStyle.UseNullableTypes;

public class NullableSamples
{
    public int NotNullValueType(int notNull) =>
        notNull;

    public int OptionalValueType(int optional = 42) =>
        optional;

    public int? NullableValueType(int? nullable) =>
        nullable;

    public int? OptionalNullableValueType(int? optionalNullable = 42) =>
        optionalNullable;

    /*
    public Enumeration NotNullEnum(Enumeration notNull) =>
        notNull;

    public Enumeration OptionalEnum(Enumeration optional = Enumeration.Member1) =>
        optional;

    public Enumeration? NullableEnum(Enumeration? nullable) =>
        nullable;

    public Enumeration? OptionalNullableEnum(Enumeration? optionalNullable = Enumeration.Member1) =>
        optionalNullable;
    */

    public string NotNullReferenceType(string notNull) =>
        notNull;

    public string OptionalReferenceType(string optional = "default") =>
        optional;

    public string? NullableReferenceType(string? nullable) =>
        nullable;

    public string? OptionalNullableReferenceType(string? optionalNullable = "default") =>
        optionalNullable;

    public Entity NotNullEntity(Entity notNull) =>
        notNull;

    public Entity? NullableEntity(Entity? nullable) =>
        nullable;

    public Entity? OptionalNullableEntity(Entity? optionalNullable = default) =>
        optionalNullable;
}