using Cruddy.Application.Models;

namespace Cruddy.Web.Repositories
{
	public interface IEmployeeRepository : IRepository<Employee>
	{
		IEnumerable<Employee> GetFilterBySearch(string? filteredDepartment, string? searchString);
	}
}