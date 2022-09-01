using ApplicationCore.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;

public class AccountController : Controller
{
	private readonly SignInManager<CruddyUser> _signInManager;

	public AccountController(SignInManager<CruddyUser> signInManager)
	{
		_signInManager = signInManager;
	}

	[HttpGet("Login")]
	public async Task<IActionResult> Login()
	{
		var loginViewModel = new LoginViewModel();
		return View(loginViewModel);
	}

	[HttpPost("Login")]
	public async Task<IActionResult> Login(LoginViewModel model)
	{
		var account = await _signInManager.UserManager.FindByNameAsync(model.Username);

		if (!ModelState.IsValid)
		{
			return View(model);
		}

		if (account != null)
		{
			var result = await _signInManager.PasswordSignInAsync(account, model.Password, true, false);

			if (result.Succeeded)
			{
				return RedirectToAction("EmployeesSorted", "Manage", new { area = "Admin" });
			}
		}

		ModelState.AddModelError("", "Username or password is incorrect.");
		return View(model);
	}

	[HttpPost("Logout")]
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