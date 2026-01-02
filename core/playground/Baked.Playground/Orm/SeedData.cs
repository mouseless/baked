namespace Baked.Playground.Orm;

public class SeedData(Entities _entities, Func<Entity> _newEntity, Parents _parents, Func<Parent> _newParent)
{
    public void Execute()
    {
        if (!_entities.By().Any())
        {
            for (int i = 0; i < 10; i++)
            {
                _newEntity().With(unique: $"seed {i}");
            }
        }

        if (!_parents.By().Any())
        {
            for (int i = 0; i < 10; i++)
            {
                _newParent().With(i % 2 == 0 ? "John" : "Jane", "Doe").Update(description: $"This is a seed data {i}");
            }
        }
    }
}