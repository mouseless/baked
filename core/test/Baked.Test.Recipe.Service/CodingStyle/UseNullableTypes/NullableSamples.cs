using Baked.Test.Business;
using Baked.Test.CodingStyle.EntityExtensionViaComposition;
using Baked.Test.CodingStyle.RichTransient;
using Baked.Test.Orm;
using Microsoft.Extensions.Logging;

namespace Baked.Test.CodingStyle.UseNullableTypes;

public class NullableSamples(ILogger<NullableSamples> _logger)
{
    public void GetValueType(int notNull, int? nullable,
        int optional = 42,
        int? optionalNullable = 42
    ) => _logger.LogInformation($"{notNull} - {nullable} - {optional} - {optionalNullable}");

    public void ValueType(int notNull, int? nullable,
        int optional = 42,
        int? optionalNullable = 42
    ) => _logger.LogInformation($"{notNull} - {nullable} - {optional} - {optionalNullable}");

    public void GetEnum(Enumeration notNull, Enumeration? nullable,
        Enumeration optional = Enumeration.Member1,
        Enumeration? optionalNullable = Enumeration.Member1
    ) => _logger.LogInformation($"{notNull} - {nullable} - {optional} - {optionalNullable}");

    public void Enum(Enumeration notNull, Enumeration? nullable,
        Enumeration optional = Enumeration.Member1,
        Enumeration? optionalNullable = Enumeration.Member1
    ) => _logger.LogInformation($"{notNull} - {nullable} - {optional} - {optionalNullable}");

    public void GetReferenceType(string notNull, string? nullable,
        string optional = "default",
        string? optionalNullable = "default"
    ) => _logger.LogInformation($"{notNull} - {nullable} - {optional} - {optionalNullable}");

    public void ReferenceType(string notNull, string? nullable,
        string optional = "default",
        string? optionalNullable = "default"
    ) => _logger.LogInformation($"{notNull} - {nullable} - {optional} - {optionalNullable}");

    public void Record(Record notNull, Record? nullable,
        Record? optionalNullable
    ) => _logger.LogInformation($"{notNull} - {nullable} - {optionalNullable}");

    public void GetEntity(Entity notNull, Entity? nullable,
        Entity? optionalNullable = default
    ) => _logger.LogInformation($"{notNull} - {nullable} - {optionalNullable}");

    public void Entity(Entity notNull, Entity? nullable,
        Entity? optionalNullable = default
    ) => _logger.LogInformation($"{notNull} - {nullable} - {optionalNullable}");

    public void GetEntityExtension(EntityExtension notNull, EntityExtension? nullable,
        EntityExtension? optionalNullable = default
    ) => _logger.LogInformation($"{notNull} - {nullable} - {optionalNullable}");

    public void EntityExtension(EntityExtension notNull, EntityExtension? nullable,
        EntityExtension? optionalNullable = default
    ) => _logger.LogInformation($"{notNull} - {nullable} - {optionalNullable}");

    public void GetRichTransient(RichTransientWithData notNull, RichTransientWithData? nullable,
       RichTransientWithData? optionalNullable = default
   ) => _logger.LogInformation($"{notNull} - {nullable} - {optionalNullable}");

    public void RichTransient(RichTransientWithData notNull, RichTransientWithData? nullable,
        RichTransientWithData? optionalNullable = default
    ) => _logger.LogInformation($"{notNull} - {nullable} - {optionalNullable}");
}