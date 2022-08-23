using ApplicationCore.Models;
using ApplicationCore.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class CruddyDbContext : IdentityDbContext<CruddyUser, CruddyRole, Guid, IdentityUserClaim<Guid>, CruddyUserRole, IdentityUserLogin<Guid>,
	IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
	public CruddyDbContext(DbContextOptions<CruddyDbContext> options)
		: base(options)
	{
	}

	public DbSet<Employee> Employees { get; set; }
	public DbSet<Department> Departments { get; set; }
}