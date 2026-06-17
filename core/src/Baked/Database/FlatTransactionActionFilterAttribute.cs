using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

using ISession = NHibernate.ISession;

namespace Baked.Database;

// TODO requires review
public class FlatTransactionActionFilterAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var session = context.HttpContext.RequestServices.GetRequiredService<ISession>();

        session.BeginTransaction();

        var actionExecutedContext = await next();

        if (actionExecutedContext.Exception != null)
        {
            await session.GetCurrentTransaction().RollbackAsync();
        }
        else
        {
            await session.GetCurrentTransaction().CommitAsync();
        }

        session.GetCurrentTransaction()?.Dispose();
    }
}