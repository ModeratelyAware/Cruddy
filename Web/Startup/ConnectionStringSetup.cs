using Microsoft.AspNetCore.Builder;

namespace Web.Startup;

public static class ConnectionStringSetup
{
	public static string BuildConnectionString(this WebApplicationBuilder builder)
	{
		var connectionString = builder.Configuration.GetConnectionString("ProdConnection");

		if (builder.Environment.IsDevelopment())
		{
			connectionString = builder.Configuration.GetConnectionString("DevConnection");
		}

		return connectionString;
	}
}