using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<IActionResult> Register()
		{
			IdentityUser user = await _userManager.FindByNameAsync("TestUser");
			if (user == null)
			{
				user = new IdentityUser();
				user.UserName = "TestUser";
			}
		}
	}
}