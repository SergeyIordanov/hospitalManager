using System.Diagnostics;
using System.Web.Mvc;
using NLog;

namespace HospitalManager.WEB.Filters
{
    public class PerfomanceFilterAttribute : FilterAttribute, IActionFilter
    {
        private readonly ILogger _logger;
        private Stopwatch _stopwatch;      

        public PerfomanceFilterAttribute()
        {
            _logger = DependencyResolver.Current.GetService<ILogger>();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _stopwatch.Stop();
            if (filterContext.RequestContext.HttpContext.Request.Url != null)
            {
                _logger.Debug(
                    $@"{filterContext.Controller}.{filterContext.ActionDescriptor.ActionName}|Perfomance of the method: {filterContext
                        .RequestContext.HttpContext.Request.Url.AbsoluteUri} is {_stopwatch.ElapsedMilliseconds} miliseconds");
            }
        }
    }
}