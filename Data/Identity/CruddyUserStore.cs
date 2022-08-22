using Cruddy.Data.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Cruddy.Data.Identity
{
	public class CruddyUserStore : IUserStore<CruddyUser>
	{
		public Task<IdentityResult> CreateAsync(CruddyUser user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IdentityResult> DeleteAsync(CruddyUser user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public Task<CruddyUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<CruddyUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetNormalizedUserNameAsync(CruddyUser user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetUserIdAsync(CruddyUser user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetUserNameAsync(CruddyUser user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetNormalizedUserNameAsync(CruddyUser user, string normalizedName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetUserNameAsync(CruddyUser user, string userName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task<IdentityResult> UpdateAsync(CruddyUser user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}