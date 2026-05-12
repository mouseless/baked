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
            for (int i = 0; i < 25; i++)
            {
                var status = i % 2 == 0 ? Status.Active : Status.Passive;
                var role = i % 2 == 0 ? Role.Admin : Role.Moderator;
                _newParent().With(i % 2 == 0 ? "John" : "Jane", "Doe", status, role).Update(description: $"This is a seed data {i}");
            }
        }
    }
}