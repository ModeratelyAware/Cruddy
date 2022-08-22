using Cruddy.Application.Models;
using Cruddy.Data;

namespace Cruddy.Web.Repositories
{
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public DepartmentRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<Department> GetAll()
		{
			return _dbContext.Departments;
		}

		public Department GetById(int? id)
		{
			return _dbContext.Departments.Find(id);
		}

		public void Insert(Department obj)
		{
			throw new NotImplementedException();
		}

		public void Update(Department obj)
		{
			_dbContext.Departments.Update(obj);
		}

		public void Update(int? id)
		{
			throw new NotImplementedException();
		}

		public void Delete(Department obj)
		{
			_dbContext.Departments.Remove(obj);
		}

		public void Delete(int? id)
		{
			Delete(GetById(id));
		}

		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}