using Baked.Database;
using Baked.Test.Orm;

namespace Baked.Test.Database;

public class TransactionSamples(
    Func<Entity> _newEntity,
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
}