namespace Baked.Communication;

public interface IClient<T>
{
    Task<Response> Send(Request request,
        bool ensureSuccessStatusCode = true
    );
}