using ApplicationCore.Models.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Web.Startup;

public static class DependencyInjectionSetup
{
	public static IServiceCollection RegisterServices(this IServiceCollection services)
	{
		services.Configure<RouteOptions>(options =>
		{
			options.LowercaseUrls = true;
			options.LowercaseQueryStrings = true;
			options.AppendTrailingSlash = true;
		});

		services.AddControllersWithViews();
		services.AddIdentity<CruddyUser, CruddyRole>().AddEntityFrameworkStores<CruddyDbContext>();

		services.ConfigureApplicationCookie(options =>
		{
			options.LoginPath = "/login";
			options.LogoutPath = "/logout";
			options.ReturnUrlParameter = options.ReturnUrlParameter.ToLower();
		});

		return services;
	}

	public static IServiceCollection RegisterDbServices(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<CruddyDbContext>(options =>
			options.UseSqlite(connectionString));

		return services;
	}
}