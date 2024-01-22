namespace Do.Test;

public interface ITestObjectService
{
    object TestObject(object request);
    Task<object> TestAsyncObject(object request);
}
