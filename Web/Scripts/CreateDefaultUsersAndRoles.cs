using ApplicationCore.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Web.Scripts;

public class CreateDefaultUsersAndRoles
{
	private readonly IServiceProvider _serviceProvider;

	public CreateDefaultUsersAndRoles(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public async Task Run()
	{
		using (var scope = _serviceProvider.CreateScope())
		{
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<CruddyRole>>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CruddyUser>>();

			var adminRoleName = "Admin";

			var adminRoleExists = await roleManager.RoleExistsAsync(adminRoleName);

			if (!adminRoleExists)
			{
				var adminRole = new CruddyRole()
				{
					Id = Guid.NewGuid(),
					Name = adminRoleName,
					NormalizedName = adminRoleName
				};

				await roleManager.CreateAsync(adminRole);
			}

			var adminUserName = "Administrator";

			var adminUserExists = await userManager.FindByNameAsync("Administrator") == null ? false : true;

			if (!adminUserExists)
			{
				var adminPassword = "Password123!";
				var adminUser = new CruddyUser
				{
					Id = Guid.NewGuid(),
					UserName = adminUserName,
				};

				var createPowerUser = await userManager.CreateAsync(adminUser, adminPassword);
				if (createPowerUser.Succeeded)
				{
					await userManager.AddToRoleAsync(adminUser, adminRoleName);
				}
			}
		}
	}
}