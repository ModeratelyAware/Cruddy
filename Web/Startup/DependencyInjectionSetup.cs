using ApplicationCore.Models.Identity;
using Infrastructure.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Web.Startup;

public static class DependencyInjectionSetup
{
	public static IServiceCollection RegisterServices(this IServiceCollection services)
	{
		services.AddControllersWithViews();
		services.AddIdentity<CruddyUser, CruddyRole>().AddEntityFrameworkStores<CruddyDbContext>();

		return services;
	}

	public static IServiceCollection RegisterDbServices(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<CruddyDbContext>(options =>
			options.UseSqlite(connectionString));

		return services;
	}
}