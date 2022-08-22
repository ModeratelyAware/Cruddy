using Cruddy.Data;
using Cruddy.Data.Identity.Models;
using Cruddy.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CruddyDbContext>(options =>
	options.UseSqlite(connectionString));

builder.Services.AddIdentity<CruddyUser, CruddyRole>().AddEntityFrameworkStores<CruddyDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
	name: "Admin",
	pattern: "{area:exists}/{controller=Manage}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "Employee",
	pattern: "{controller=Employee}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

await CreateRoles(app.Services);
app.Run();

async Task CreateRoles(IServiceProvider serviceProvider)
{
	using (var scope = serviceProvider.CreateScope())
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