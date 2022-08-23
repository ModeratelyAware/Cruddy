using ApplicationCore.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Attributes;
using Web.ViewModels;

namespace Web.Controllers;

[ModelStateValidation]
public class AccountController : Controller
{
	private readonly SignInManager<CruddyUser> _signInManager;

	public AccountController(SignInManager<CruddyUser> signInManager)
	{
		_signInManager = signInManager;
	}

	public async Task<IActionResult> Login()
	{
		var loginViewModel = new LoginViewModel();
		return View(loginViewModel);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginViewModel loginViewModel)
	{
		var account = await _signInManager.UserManager.FindByNameAsync(loginViewModel.Username);

		if (account != null)
		{
			var result = await _signInManager.PasswordSignInAsync(account, loginViewModel.Password, true, false);

			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Manage", new { area = "Admin" });
			}
		}

		ModelState.AddModelError("", "Username or password is incorrect.");
		return View(loginViewModel);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Logout()
	{
		var principal = HttpContext.User;

		var signedIn = _signInManager.IsSignedIn(principal);
		if (signedIn)
		{
			await _signInManager.SignOutAsync();
		}

		return RedirectToAction("Index", "Home", new { area = "" });
	}
}