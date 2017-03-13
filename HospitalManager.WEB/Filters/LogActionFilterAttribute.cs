using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using NLog;

namespace HospitalManager.WEB.Filters
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public LogActionFilterAttribute()
        {
            _logger = DependencyResolver.Current.GetService<ILogger>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.Url == null)
            {
                return;
            }

            var logResult = $"{filterContext.Controller}.{filterContext.ActionDescriptor.ActionName}|, With arguments: ";

            foreach (var param in filterContext.ActionParameters)
            {
                if (param.GetType().IsValueType || param.Value is string)
                {
                    logResult += $"Name: {param.Key} | Value: {param.Value} ";
                    continue;
                }

                var objParams = param.Value.GetType().GetProperties(
                    BindingFlags.Public | BindingFlags.Instance);

                logResult = objParams.Aggregate(logResult, (current, propertyInfo) => current + $"Name: {propertyInfo.Name} | Value: {propertyInfo.GetValue(param.Value)}, ");
            }

            _logger.Debug(logResult);
        }
    }
}