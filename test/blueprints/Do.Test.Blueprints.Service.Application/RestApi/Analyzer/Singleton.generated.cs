// This file will be auto-generated

using Do.Orm;
using Microsoft.AspNetCore.Mvc;

namespace Do.Test;

[ApiController]
public class SingletonController
{
    [HttpGet]
    [Produces("application/json")]
    [Route("singleton/time")]
    public DateTime GetNow([FromServices] Singleton service)
    {
        var result = service.GetNow();

        return result;
    }

    [HttpPost]
    [Produces("application/json")]
    [Route("singleton/test-transaction-action")]
    public async Task TestTransactionAction([FromServices] Singleton service)
    {
        await service.TestTransactionAction();
    }

    [HttpPost]
    [Produces("application/json")]
    [Route("singleton/test-transaction-func")]
    public async Task TestTransactionFunc([FromServices] Singleton service)
    {
        await service.TestTransactionFunc();
    }

    [HttpPost]
    [Produces("application/json")]
    [Route("singleton/test-exception")]
    public void TestException([FromServices] Singleton service, bool handled)
    {
        service.TestException(handled);
    }

    public record TestTransactionNullableRequest(Guid? EntityId = default);

    [HttpPost]
    [Produces("application/json")]
    [Route("singleton/test-transaction-nullable")]
    public async Task TestTransactionNullable([FromServices] Singleton singletonService, [FromServices] IQueryContext<Entity> entityService, [FromBody] TestTransactionNullableRequest request)
    {
        await singletonService.TestTransactionNullable(
            entity: request.EntityId.HasValue
                ? entityService.SingleById(request.EntityId.Value)
                : null
        );
    }

    [HttpPut]
    [Produces("application/json")]
    [Route("singleton/test-async-object")]
    public async Task<object> TestAsyncObject([FromServices] Singleton service, [FromBody] object request)
    {
        var result = await service.TestAsyncObject(request);

        return result;
    }

    [HttpPut]
    [Produces("application/json")]
    [Route("singleton/test-object")]
    public object TestObject([FromServices] Singleton service, [FromBody] object request)
    {
        var result = service.TestObject(request);

        return result;
    }
}
