
namespace Do.Test;

public interface IInterface
{
    object TestObject(object request);

    Task<object> TestAsyncObject(object request);
}
