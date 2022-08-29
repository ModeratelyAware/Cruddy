namespace Web.Startup;

public static class EndpointsSetup
{
	public static WebApplication MapEndpoints(this WebApplication app)
	{
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

		return app;
	}
}