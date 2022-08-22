﻿using Cruddy.Application.Models;
using Cruddy.Data;
using Microsoft.EntityFrameworkCore;

namespace Cruddy.Web.Repositories
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly CruddyDbContext _dbContext;

		public EmployeeRepository(CruddyDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IQueryable<Employee>> GetAll()
		{
			return _dbContext.Employees.Include(e => e.Department);
		}

		public async Task<IQueryable<Employee>> GetAllFiltered(string? filteredDepartment, string? searchString)
		{
			var employees = _dbContext.Employees.Include(e => e.Department);
			employees = FilterByDepartment(employees, filteredDepartment).Include(e => e.Department);
			employees = FilterBySearch(employees, searchString).Include(e => e.Department);
			OrderByTitle(employees, out var ordered, out var remainder);
			employees = ordered.Concat(remainder).Include(e => e.Department);
			return employees;
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

		private IQueryable<Employee> FilterBySearch(IQueryable<Employee> employees, string? searchString)
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

		private IQueryable<Employee> FilterByDepartment(IQueryable<Employee> employees, string? filteredDepartment)
		{
			if (filteredDepartment != null)
			{
				filteredDepartment = filteredDepartment.ToLower();
				employees = employees
							.Where(e => e.Department!.Name.ToLower().Contains(filteredDepartment));
			}
			return employees;
		}

		private void OrderByTitle(IQueryable<Employee> employees, out IQueryable<Employee> ordered, out IQueryable<Employee> remainder)
		{
			ordered = employees
				.OrderBy(e => IndexEmployeeByTitle(e.Title))
				.ThenBy(e => e.LastName);

			var indexedEmployees = employees.AsEnumerable()
							.Select(e => new { Employee = e, Index = IndexEmployeeByTitle(e.Title.ToLower()) })
							.OrderBy(e => e.Index)
							.ThenBy(e => e.Employee.LastName);

			ordered = indexedEmployees
						.Where(e => e.Index >= 0)
						.Select(e => e.Employee)
						.AsQueryable();

			remainder = indexedEmployees
						.Where(e => e.Index == -1)
						.Select(e => e.Employee)
						.AsQueryable();
		}

		private int IndexEmployeeByTitle(string employeeTitle)
		{
			employeeTitle = employeeTitle.Replace(" ", string.Empty);
			foreach (var value in Enum.GetValues(typeof(EmployeeTitle)))
			{
				if (employeeTitle == value.ToString()!.ToLower())
				{
					return (int)value;
				};
			};
			return -1;
		}
	}

	public enum EmployeeTitle
	{
		VP,
		Director,
		AssistantDirector,
		Manager,
		AssistantManager,
		Supervisor
	}
}