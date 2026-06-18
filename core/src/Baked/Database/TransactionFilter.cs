using Microsoft.AspNetCore.Mvc.Filters;
using NHibernate;

using ISession = NHibernate.ISession;

namespace Baked.Database;

public class TransactionFilter(ISession session)
    : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        session.BeginTransaction();

        var success = true;
        try
        {
            var executedContext = await next();
            success = executedContext.Exception is null;
        }
        catch
        {
            success = false;

            throw;
        }
        finally
        {
            using var tx = session.GetCurrentTransaction();
            if (tx is not null)
            {
                if (success)
                {
                    await tx.CommitAsync();
                }
                else
                {
                    await tx.RollbackAsync();
                }
            }
        }
    }
}