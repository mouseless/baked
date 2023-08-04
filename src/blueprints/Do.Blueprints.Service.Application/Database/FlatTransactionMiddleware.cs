using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

using ISession = NHibernate.ISession;

namespace Do.Database;

public class FlatTransactionMiddleware
{
    readonly RequestDelegate _next;

    public FlatTransactionMiddleware(RequestDelegate next) =>
        _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        using (var session = context.RequestServices.GetRequiredService<ISession>())
        {
            session.BeginTransaction();

            try
            {
                await _next(context);
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
}
