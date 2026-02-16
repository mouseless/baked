using Microsoft.AspNetCore.Mvc.Filters;

namespace Baked.CodingStyle.Locatable;

public class InitializeLocatablesFilter(Func<LocatableInitializations> _getLocatableInitializations)
    : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var initializations = _getLocatableInitializations();
        await Task.WhenAll(initializations.Select(initialize => initialize()));
        initializations.Clear();

        await next();
    }
}