namespace Baked.Playground.Orm;

public record RecordWithEntity(
    IEnumerable<Entity> Entities,
    Entity[] OtherEntities
);