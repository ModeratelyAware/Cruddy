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

		public async Task<Department> GetById(int? id)
		{
			var department = await _dbContext.Departments.FindAsync(id);
			return department;
		}

		public async Task Insert(Department obj)
		{
			await _dbContext.Departments.AddAsync(obj);
		}

		public async Task Update(Department obj)
		{
			_dbContext.Departments.Update(obj);
		}

		public async Task Update(int? id)
		{
			await Update(await GetById(id));
		}

		public async Task Delete(Department obj)
		{
			_dbContext.Departments.Remove(obj);
		}

		public async Task Delete(int? id)
		{
			await Delete(await GetById(id));
		}

		public async Task Save()
		{
			await _dbContext.SaveChangesAsync();
		}
	}
}