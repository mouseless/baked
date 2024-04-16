namespace Do.Communication;

public interface IClient<T>
{
    Task<Response> Send(Request request);
}