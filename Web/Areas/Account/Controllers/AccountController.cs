using Cruddy.Data;
using Cruddy.Application.Models;
using Cruddy.Web.Filters;
using Cruddy.Web.Repositories;
using Cruddy.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Cruddy.Data.Identity.Models;
using System.Security.Claims;

namespace Web.Areas.AdminPortal.Controllers
{
	[Area("Account")]
	[ModelStateActionFilter]
	public class AccountController : Controller
	{
		private readonly SignInManager<CruddyUser> _signInManager;

		public AccountController(SignInManager<CruddyUser> signInManager)
		{
			_signInManager = signInManager;
		}

		public IActionResult Login()
		{
			var accountVM = new AccountViewModel();
			return View(accountVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(CruddyUser user, string password)
		{
			var account = await _signInManager.UserManager.FindByNameAsync(user.UserName);
			var result = await _signInManager.PasswordSignInAsync(account, password, true, false);

			if (!result.Succeeded)
			{
				return RedirectToAction("Login");
			}
			return RedirectToAction("Index", "Manage", new { area = "Admin" });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			var principal = HttpContext.User as ClaimsPrincipal;
			if (_signInManager.IsSignedIn(principal))
			{
				await _signInManager.SignOutAsync();
			}
			return RedirectToAction("Index", "Home", new { area = "" });
		}
	}
}