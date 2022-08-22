using Cruddy.Data.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Cruddy.Data.Identity
{
	public class CruddyRoleStore : IRoleStore<CruddyRole>
	{
		public Task<IdentityResult> CreateAsync(CruddyRole role, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IdentityResult> DeleteAsync(CruddyRole role, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public Task<CruddyRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<CruddyRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetNormalizedRoleNameAsync(CruddyRole role, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetRoleIdAsync(CruddyRole role, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetRoleNameAsync(CruddyRole role, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetNormalizedRoleNameAsync(CruddyRole role, string normalizedName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetRoleNameAsync(CruddyRole role, string roleName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IdentityResult> UpdateAsync(CruddyRole role, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}