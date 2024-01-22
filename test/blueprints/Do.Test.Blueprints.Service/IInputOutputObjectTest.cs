namespace Do.Test;

public interface IInputOutputObjectTest
{
    object TestObject(object request);
    Task<object> TestAsyncObject(object request);
}
