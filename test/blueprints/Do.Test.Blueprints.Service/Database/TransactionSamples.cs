using Do.Database;
using Do.Test.DataAccess;

namespace Do.Test.Database;

public class TransactionSamples(
    Func<Entity> _newEntity,
    ITransaction _transaction,
    TimeProvider _timeProvider
)
{
    public async Task CommitAction()
    {
        await _transaction.CommitAsync(() =>
        {
            // do not remove this variable, this is to ensure call is made to `Action` overload
            var _ = _newEntity().With(
                guid: Guid.NewGuid(),
                @string: "test",
                stringData: "transaction action",
                int32: 1,
                unique: $"{Guid.NewGuid()}"[8..],
                uri: new("https://action.com"),
                @dynamic: new { transaction = "action" },
                @enum: Status.Enabled,
                dateTime: _timeProvider.GetNow()
            );
        });

        throw new();
    }

    public void Rollback(string @string)
    {
        _newEntity().With(
            guid: Guid.NewGuid(),
            @string: @string,
            stringData: "transaction func",
            int32: 1,
            unique: $"{Guid.NewGuid()}"[8..],
            uri: new("https://func.com"),
            @dynamic: new { transaction = "func" },
            @enum: Status.Enabled,
            dateTime: _timeProvider.GetNow()
        );

        throw new();
    }

    public async Task CommitFunc()
    {
        var entity = await _transaction.CommitAsync(() =>
            _newEntity().With(
                guid: Guid.NewGuid(),
                @string: "test",
                stringData: "transaction func",
                int32: 1,
                unique: $"{Guid.NewGuid()}"[8..],
                uri: new("https://func.com"),
                @dynamic: new { transaction = "func" },
                @enum: Status.Enabled,
                dateTime: _timeProvider.GetNow()
            )
        );

        await entity.Update(
            guid: Guid.NewGuid(),
            @string: "rollback",
            stringData: "rollback",
            int32: 2,
            unique: $"{Guid.NewGuid()}"[8..],
            uri: new("https://rollback.com"),
            @dynamic: new { rollback = "rollback" },
            @enum: Status.Disabled,
            dateTime: _timeProvider.GetNow()
        );

        throw new();
    }

    public async Task CommitNullable(Entity? entity)
    {
        await _transaction.CommitAsync(entity, entity =>
             entity.Update(
                guid: Guid.NewGuid(),
                @string: "test",
                stringData: "transaction nullable",
                int32: 1,
                unique: $"{Guid.NewGuid()}"[8..],
                uri: new("https://func.com"),
                @dynamic: new { transaction = "func" },
                @enum: Status.Enabled,
                dateTime: _timeProvider.GetNow()
            )
        );
    }
}
