using Do.Core;
using Do.Database;
using Do.ExceptionHandling;

namespace Do.Test;

public class Singleton
{
    readonly ISystem _system;
    readonly Func<Entity> _newEntity;
    readonly ITransaction _transaction;

    public Singleton(ISystem system, Func<Entity> newEntity, ITransaction transaction)
    {
        _system = system;
        _newEntity = newEntity;
        _transaction = transaction;
    }

    public DateTime GetNow() => _system.Now;

    public void TestException(bool handled)
    {
        if (handled)
        {
            throw new HandledException("A handled exception was thrown");
        }

        throw new InvalidOperationException();
    }

    public async Task TestTransactionAction()
    {
        await _transaction.CommitAsync(() =>
        {
            // do not remove this variable, this is to ensure call is made to `Action` overload
            var _ = _newEntity().With(
                guid: Guid.NewGuid(),
                @string: "test",
                stringData: "transaction action",
                int32: 1,
                uri: new("https://action.com"),
                @dynamic: new { transaction = "action" },
                @enum: Status.Enabled,
                dateTime: DateTime.Now
            );
        });

        throw new();
    }

    public async Task TestTransactionFunc()
    {
        var entity = await _transaction.CommitAsync(() =>
            _newEntity().With(
                guid: Guid.NewGuid(),
                @string: "test",
                stringData: "transaction func",
                int32: 1,
                uri: new("https://func.com"),
                @dynamic: new { transaction = "func" },
                @enum: Status.Enabled,
                dateTime: DateTime.Now
            )
        );

        await entity.Update(
            guid: Guid.NewGuid(),
            @string: "rollback",
            stringData: "rollback",
            int32: 2,
            uri: new("https://rollback.com"),
            @dynamic: new { rollback = "rollback" },
            @enum: Status.Disabled,
            dateTime: DateTime.Now
        );

        throw new();
    }

    public object TestObject(object request) => request;

    public async Task<object> TestAsyncObject(object request)
    {
        await Task.Delay(100);

        return request;
    }

    public async Task TestTransactionNullable(Entity? entity)
    {
        await _transaction.CommitAsync(entity, entity =>
             entity.Update(
                guid: Guid.NewGuid(),
                @string: "test",
                stringData: "transaction func",
                int32: 1,
                uri: new("https://func.com"),
                @dynamic: new { transaction = "func" },
                @enum: Status.Enabled,
                dateTime: DateTime.Now
            )
        );
    }
}
