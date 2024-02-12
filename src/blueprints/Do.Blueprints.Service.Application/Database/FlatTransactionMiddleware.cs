using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

using ISession = NHibernate.ISession;

namespace Do.Database;

public class FlatTransactionMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var metadata = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata;

        if (metadata?.GetMetadata<NoTransactionAttribute>() is not null)
        {
            await _next(context);
        }
        else
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
}
