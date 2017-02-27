using System.Web.Mvc;
using NLog;

namespace HospitalManager.WEB.Filters
{
    public class LogIpFilterAttribute : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public LogIpFilterAttribute()
        {
            _logger = DependencyResolver.Current.GetService<ILogger>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.Url != null)
            {
                _logger.Debug(
                    $@"{filterContext.Controller}.{filterContext.ActionDescriptor.ActionName}|Method uri:{filterContext
                        .RequestContext.HttpContext.Request.Url.AbsoluteUri}, " +
                    $@"Client IP: {filterContext.HttpContext.Request.UserHostAddress}, ");
            }
        }
    }
}