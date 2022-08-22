using Cruddy.Data;
using Cruddy.Data.Identity.Models;
using Cruddy.Web.Repositories;
using Cruddy.Web.Scripts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CruddyDbContext>(options =>
	options.UseSqlite(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<CruddyUser, CruddyRole>().AddEntityFrameworkStores<CruddyDbContext>();

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

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "Admin",
		pattern: "{area:exists}/{controller=Manage}/{action=Index}/{id?}");

	endpoints.MapControllerRoute(
		name: "Employees",
		pattern: "{area:exists}/{controller=Display}/{action=Index}/{id?}");

	endpoints.MapControllerRoute(
		name: "Account",
		pattern: "{controller=Account}/{action=Index}/{id?}");

	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
});

await new CreateDefaultUsersAndRoles(app.Services).Run();

app.Run();