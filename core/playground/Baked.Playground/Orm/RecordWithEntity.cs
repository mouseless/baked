namespace Baked.Playground.Orm;

public record RecordWithEntity(
    Entity Single,
    IEnumerable<Entity> Enumerable,
    Entity[] Array
);