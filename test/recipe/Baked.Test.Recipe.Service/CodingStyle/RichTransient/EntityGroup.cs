using Baked.Test.Orm;

namespace Baked.Test.CodingStyle.RichTransient;

public class EntityGroup
{
    public EntityGroup With(string id)
    {
        Id = id;

        return this;
    }

    public string Id { get; set; } = default!;

    public List<Entity> GetEntities()
    {
        return new List<Entity>();
    }
}