using Microsoft.AspNetCore.Mvc.Filters;

namespace Baked.CodingStyle.Locatable;

public class InitializeLocatablesFilter(LocatableInitializations _locatableInitializations)
    : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await Task.WhenAll(_locatableInitializations);
        await next();
    }
}