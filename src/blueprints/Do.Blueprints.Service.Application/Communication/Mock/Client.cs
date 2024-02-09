
namespace Do.Communication.Mock;

public class Client<T>(ResponseGenerator _responseGenerator)
    : IClient<T>
{
    public Task<Response> Send(Request request) =>
        Task.FromResult(_responseGenerator.Generate(request));
}
