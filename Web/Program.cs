using ApplicationCore.Models.Identity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Web.Startup;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.BuildConnectionString();

builder.Services.RegisterDbServices(connectionString);
builder.Services.RegisterServices();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

app.CreateBuiltinAdminAccount();

app.Run();