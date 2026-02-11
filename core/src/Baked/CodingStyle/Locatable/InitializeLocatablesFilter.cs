using Microsoft.AspNetCore.Mvc.Filters;

namespace Baked.CodingStyle.Locatable;

public class InitializeLocatablesFilter(Func<LocatableInitializations> _getLocatableInitializations)
    : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await Task.WhenAll(_getLocatableInitializations().Select(initialize => initialize()));
        await next();
    }
}