using Baked.ExceptionHandling;
using System.Net;

namespace Baked.Orm;

public class RecordNotFoundException(Type entityType, string field, object value, bool notFound)
    : HandledException($"{entityType.Name} with {field}: '{value}' does not exist")
{
    public static RecordNotFoundException For<T>(Guid id,
        bool notFound = false
    ) => new(typeof(T), id, notFound);

    public static RecordNotFoundException For<T>(string field, object value,
        bool notFound = false
    ) => new(typeof(T), field, value, notFound);

    public override HttpStatusCode StatusCode => notFound ? HttpStatusCode.NotFound : base.StatusCode;
    public override string LKey => "NAME_with_FIELD_VALUE_does_not_exist";
    public override string[] LParams => [entityType.Name, field, value.ToString()!];

    public RecordNotFoundException(Type entityType, Guid id, bool notFound)
        : this(entityType, "Id", $"{id}", notFound) { }
}