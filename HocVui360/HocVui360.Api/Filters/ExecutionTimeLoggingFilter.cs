using Microsoft.AspNetCore.Mvc.Filters;

namespace HocVui360.Api.Filters;

public class ExecutionTimeLoggingFilter : IActionFilter, IAsyncActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        throw new NotImplementedException();
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        OnActionExecuting(context);

        OnActionExecuted(await next());
    }
}