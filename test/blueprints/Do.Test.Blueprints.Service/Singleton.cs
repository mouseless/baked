using Do.Communication;
using Do.Database;
using Newtonsoft.Json;

namespace Do.Test;

public class Singleton(
    TimeProvider _timeProvider,
    Func<Entity> _newEntity,
    ITransaction _transaction,
    Func<OperationWithGenericParameter<Entity>> _newOperationWithGenericParameter,
    IClient<Singleton> _client
) : SingletonBase, IInterface
{
    public override DateTime GetTime() =>
        _timeProvider.GetNow();

    public string TestOperationWithGenericParameter(string parameter)
    {
        return _newOperationWithGenericParameter()
            .With(parameter)
            .Execute();
    }

    public async Task<List<PullRequest>> TestClient()
    {
        var request = new Request("repos/mouseless/do/pulls", HttpMethod.Get);

        var response = await _client.Send(request);

        return JsonConvert.DeserializeObject<List<PullRequest>>(response.Content) ?? [];
    }

    public void TestException(bool handled)
    {
        if (handled)
        {
            throw new TestServiceHandledException("A handled exception was thrown");
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
                unique: $"{Guid.NewGuid()}"[8..],
                uri: new("https://action.com"),
                @dynamic: new { transaction = "action" },
                @enum: Status.Enabled,
                dateTime: GetTime()
            );
        });

        throw new();
    }

    public void TestTransactionRollback(string @string)
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
            dateTime: GetTime()
        );

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
                unique: $"{Guid.NewGuid()}"[8..],
                uri: new("https://func.com"),
                @dynamic: new { transaction = "func" },
                @enum: Status.Enabled,
                dateTime: GetTime()
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
            dateTime: GetTime()
        );

        throw new();
    }

    public async Task TestTransactionNullable(Entity? entity)
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
                dateTime: GetTime()
            )
        );
    }

    public object TestFormPostAuthentication(object value) => value;
}
