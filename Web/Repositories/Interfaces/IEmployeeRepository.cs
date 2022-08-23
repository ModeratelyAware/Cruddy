using Cruddy.Application.Models;

namespace Cruddy.Web.Repositories
{
	public interface IEmployeeRepository
	{
		Task<IQueryable<Employee>> GetAll();

		Task<IQueryable<Employee>> GetAllFiltered(string? filteredDepartment, string? searchString);

		Task<Employee> GetById(int? id);

		Task Insert(Employee obj);

		Task Update(Employee obj);

		Task Update(int? id);

		Task Delete(Employee obj);

		Task Delete(int? id);

		Task Save();
	}
}