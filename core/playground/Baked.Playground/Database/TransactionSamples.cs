using Baked.Database;
using Baked.Playground.Orm;

namespace Baked.Playground.Database;

public class TransactionSamples(
    Func<Entity> _newEntity,
    Func<Parent> _newParent,
    Func<Child> _newChild,
    ITransaction _transaction
)
{
    public async Task CommitAction()
    {
        await _transaction.CommitAsync(() =>
        {
            // do not remove this variable, this is to ensure call is made to `Action` overload
            var _ = _newEntity().With();
        });

        throw new();
    }

    public async Task CommitFunc()
    {
        var entity = await _transaction.CommitAsync(() =>
            _newEntity().With()
        );

        await entity.Update();

        throw new();
    }

    public async Task CommitNullable(Entity? entity, string @string)
    {
        await _transaction.CommitAsync(entity, entity =>
             entity.Update(@string: @string)
        );
    }

    public void Rollback(string @string)
    {
        _newEntity().With(@string: @string);

        throw new();
    }

    public void Rollback_multiple(string @string)
    {
        var parent = _newParent().With("ParentName: " + @string, "ParentSurname: " + @string, Status.Active, Role.Admin);
        _newChild().With(parent.Name + " Child", parent);

        throw new();
    }
}