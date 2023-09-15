// This file will be auto-generated

using Microsoft.AspNetCore.Mvc;

namespace Do.Test;

[ApiController]
public class SingletonController
{
    readonly IServiceProvider _serviceProvider;

    public SingletonController(IServiceProvider serviceProvider) =>
        _serviceProvider = serviceProvider;

    [HttpGet]
    [Produces("application/json")]
    [Route("singleton/time")]
    public DateTime GetNow()
    {
        var target = _serviceProvider.GetRequiredService<Singleton>();

        return target.GetNow();
    }

    [HttpPost]
    [Produces("application/json")]
    [Route("singleton/test-transaction-action")]
    public async Task TestTransactionAction()
    {
        var target = _serviceProvider.GetRequiredService<Singleton>();

        await target.TestTransactionAction();
    }

    [HttpPost]
    [Produces("application/json")]
    [Route("singleton/test-transaction-func")]
    public async Task TestTransactionFunc()
    {
        var target = _serviceProvider.GetRequiredService<Singleton>();

        await target.TestTransactionFunc();
    }

    [HttpPost]
    [Produces("application/json")]
    [Route("singleton/test-exception")]
    public void TestException(bool handled)
    {
        var target = _serviceProvider.GetRequiredService<Singleton>();

        target.TestException(handled);
    }

    [HttpPut]
    [Produces("application/json")]
    [Route("singleton/test-async-object")]
    public async Task<object> TestObject([FromBody] object request)
    {
        var singleton = _serviceProvider.GetRequiredService<Singleton>();

        var target = await singleton.TestAsyncObject(request);

        return target;
    }

    [HttpPut]
    [Produces("application/json")]
    [Route("singleton/test-object")]
    public object TestAsyncObject([FromBody] object request)
    {
        var target = _serviceProvider.GetRequiredService<Singleton>();

        return target.TestObject(request);
    }
}
