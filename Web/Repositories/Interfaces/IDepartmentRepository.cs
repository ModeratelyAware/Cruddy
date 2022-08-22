using Cruddy.Application.Models;

namespace Cruddy.Web.Repositories
{
	public interface IDepartmentRepository
	{
		Task<IQueryable<Department>> GetAll();

		Task<IQueryable<string>> GetAllByName();

		Department GetById(int? id);

		void Insert(Department obj);

		void Update(Department obj);

		void Update(int? id);

		void Delete(Department obj);

		void Delete(int? id);

		void Save();
	}
}