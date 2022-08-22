using Cruddy.Application.Models;

namespace Cruddy.Web.Repositories
{
	public interface IEmployeeRepository
	{
		Task<IQueryable<Employee>> GetAll();

		Task<IQueryable<Employee>> GetAllFiltered(string? filteredDepartment, string? searchString);

		Employee GetById(int? id);

		void Insert(Employee obj);

		void Update(Employee obj);

		void Update(int? id);

		void Delete(Employee obj);

		void Delete(int? id);

		void Save();
	}
}