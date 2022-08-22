using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Cruddy.Web.Filters
{
	public class ModelStateActionFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			Log("Action executing...", context.RouteData);
		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			if (!context.ModelState.IsValid)
			{
				Log("Action execution FAILED", context.RouteData);
				return;
			}
			Log("Action execution SUCCESS", context.RouteData);
		}

		private void Log(string methodName, RouteData routeData)
		{
			var controllerName = routeData.Values["controller"];
			var actionName = routeData.Values["action"];
			var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
			Debug.WriteLine(message, "Action Filter Log");
		}
	}
}