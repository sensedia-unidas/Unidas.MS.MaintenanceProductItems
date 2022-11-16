using Microsoft.AspNetCore.Mvc.Filters;
using OuroVerde.Maintenance.Services.Api.Filters.Traces;

namespace Unidas.MS.Maintenance.Services.Api.Filters.Traces
{
    public class GlobalControllerAppInsightsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            GlobalStoredTraces.AddRequestTrace(context.HttpContext);
            base.OnActionExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            GlobalStoredTraces.AddResponseTrace(context);
            base.OnResultExecuted(context);
        }
    }
}
