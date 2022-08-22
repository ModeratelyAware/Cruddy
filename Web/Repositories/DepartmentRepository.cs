using Cruddy.Application.Models;
using Cruddy.Data;
using Microsoft.EntityFrameworkCore;

namespace Cruddy.Web.Repositories
{
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly CruddyDbContext _dbContext;

		public DepartmentRepository(CruddyDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IQueryable<Department>> GetAll()
		{
			return _dbContext.Departments;
		}

		public async Task<IQueryable<string>> GetAllByName()
		{
			return _dbContext.Departments.Select(d => d.Name);
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