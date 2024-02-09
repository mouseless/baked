namespace Do.Communication.Mock;

public class Client<T>(Func<Response> _getDefaultResponse) : IClient<T>
{
    public Task<Response> Send(Request request) => Task.FromResult(_getDefaultResponse());
}
