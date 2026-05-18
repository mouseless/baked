using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

using ISession = NHibernate.ISession;

namespace Baked.Database;

public class FlatTransactionActionFilterAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        using var session = context.HttpContext.RequestServices.GetRequiredService<ISession>();

        session.BeginTransaction();

        try
        {
            await next();
            await session.GetCurrentTransaction().CommitAsync();
        }
        catch
        {
            await session.GetCurrentTransaction().RollbackAsync();

            throw;
        }
        finally
        {
            session.GetCurrentTransaction()?.Dispose();
        }
    }
}