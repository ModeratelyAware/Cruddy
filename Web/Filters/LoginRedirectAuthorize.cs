using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cruddy.Web.Filters
{
	public class LoginRedirectedAuthorize : ActionFilterAttribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			Console.WriteLine(context.Result);
		}
	}
}