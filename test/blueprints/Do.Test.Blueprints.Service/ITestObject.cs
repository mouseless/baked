namespace Do.Test;

public interface ITestObject
{
    object TestObject(object request);
    Task<object> TestAsyncObject(object request);
}
