using Baked.Business;
using Baked.ExceptionHandling;
using System.Net;

namespace Baked.Orm;

public class RecordNotFoundException(Type entityType, string field, object value, bool notFound)
    : HandledException(
        "{0} with {1}: '{2}' does not exist",
        extraData: new()
        {
            ["name"] = entityType.Name,
            ["field"] = field,
            ["value"] = value.ToString()
        }
    )
{
    public static RecordNotFoundException For<T>(Id id,
        bool notFound = false
    ) => new(typeof(T), id, notFound);

    public static RecordNotFoundException For<T>(string field, object value,
        bool notFound = false
    ) => new(typeof(T), field, value, notFound);

    public override HttpStatusCode StatusCode => notFound ? HttpStatusCode.NotFound : base.StatusCode;

    public RecordNotFoundException(Type entityType, Id id, bool notFound)
        : this(entityType, "Id", $"{id}", notFound) { }
}