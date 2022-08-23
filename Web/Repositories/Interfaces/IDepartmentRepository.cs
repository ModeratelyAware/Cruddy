using Cruddy.Application.Models;

namespace Cruddy.Web.Repositories
{
	public interface IDepartmentRepository
	{
		Task<IQueryable<Department>> GetAll();

		Task<IQueryable<string>> GetAllByName();

		Task<Department> GetById(int? id);

		Task Insert(Department obj);

		Task Update(Department obj);

		Task Update(int? id);

		Task Delete(Department obj);

		Task Delete(int? id);

		Task Save();
	}
}