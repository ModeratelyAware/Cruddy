using Cruddy.Application.Models;

namespace Cruddy.Web.Repositories
{
	public interface IRepository<T>
	{
		IEnumerable<T> GetAll();

		T GetById(int? id);

		void Insert(T obj);

		void Update(T obj);

		void Update(int? id);

		void Delete(T obj);

		void Delete(int? id);

		void Save();
	}
}