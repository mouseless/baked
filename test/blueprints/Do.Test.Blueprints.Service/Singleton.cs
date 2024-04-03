using Do.Communication;
using Do.Database;
using Newtonsoft.Json;

namespace Do.Test;

public class Singleton(
    TimeProvider _timeProvider,
    Func<OperationWithGenericParameter<Entity>> _newOperationWithGenericParameter,
    IClient<Singleton> _client
) : SingletonBase(_timeProvider), IInterface
{
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

    public object TestFormPostAuthentication(object value) => value;
}
