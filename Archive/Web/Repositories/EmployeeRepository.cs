using Cruddy.Application.Models;
using Cruddy.Data;
using Microsoft.EntityFrameworkCore;

namespace Cruddy.Web.Repositories
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly ApplicationDbContext _dbContext;

		public EmployeeRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<Employee> GetAll()
		{
			return _dbContext.Employees.Include(e => e.Department);
		}

		public Employee GetById(int? id)
		{
			return _dbContext.Employees.Find(id);
		}

		public void Insert(Employee obj)
		{
			_dbContext.Employees.Add(obj);
		}

		public void Update(Employee obj)
		{
			_dbContext.Employees.Update(obj);
		}

		public void Update(int? id)
		{
			Update(GetById(id));
		}

		public void Delete(Employee obj)
		{
			_dbContext.Employees.Remove(obj);
		}

		public void Delete(int? id)
		{
			Delete(GetById(id));
		}

		public void Save()
		{
			_dbContext.SaveChanges();
		}

		public IEnumerable<Employee> GetFilterBySearch(string? filteredDepartment, string? searchString)
		{
			var employees = GetAll();
			employees = FilterByDepartment(employees, filteredDepartment);
			employees = FilterBySearch(employees, searchString);
			OrderByTitle(employees, out var ordered, out var remainder);
			employees = ordered.Concat(remainder);
			return employees;
		}

		private IEnumerable<Employee> FilterBySearch(IEnumerable<Employee> employees, string? searchString)
		{
			if (searchString != null)
			{
				searchString = searchString.ToLower();
				employees = employees
							.Where(e => e.FirstName != null && e.FirstName.ToLower().Contains(searchString)
								|| e.LastName != null && e.LastName.ToLower().Contains(searchString)
								|| e.Ext != null && e.Ext.ToString()!.ToLower().Contains(searchString)
								|| e.Phone1 != null && e.Phone1.ToLower().Contains(searchString)
								|| e.Phone2 != null && e.Phone2.ToLower().Contains(searchString));
			}
			return employees;
		}

		private IEnumerable<Employee> FilterByDepartment(IEnumerable<Employee> employees, string? filteredDepartment)
		{
			if (filteredDepartment != null)
			{
				filteredDepartment = filteredDepartment.ToLower();
				employees = employees
							.Where(e => e.Department!.Name.ToLower().Contains(filteredDepartment));
			}
			return employees;
		}

		private void OrderByTitle(IEnumerable<Employee> employees, out IEnumerable<Employee> ordered, out IEnumerable<Employee> remainder)
		{
			var titles = new string[] { "VP", "Director", "Assistant Director", "Manager", "Assistant Manager", "Supervisor" };
			var indexedEmployees = employees
							.Select(e => new { Employee = e, Index = Array.IndexOf(titles, e.Title) })
							.OrderBy(e => e.Index)
							.ThenBy(e => e.Employee.LastName);

			ordered = indexedEmployees
						.Where(e => e.Index >= 0)
						.Select(e => e.Employee);

			remainder = indexedEmployees
						.Where(e => e.Index == -1)
						.Select(e => e.Employee);
		}
	}
}