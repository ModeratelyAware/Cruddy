using ApplicationCore.Models.Identity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Web.Scripts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ProdConnection");

if (builder.Environment.IsDevelopment())
{
	connectionString = builder.Configuration.GetConnectionString("DevConnection");
}

builder.Services.AddDbContext<CruddyDbContext>(options =>
	options.UseSqlite(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<CruddyUser, CruddyRole>().AddEntityFrameworkStores<CruddyDbContext>();

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