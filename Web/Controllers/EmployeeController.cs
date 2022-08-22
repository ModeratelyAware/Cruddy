using Cruddy.Data;
using Cruddy.Application.Models;
using Cruddy.Web.Filters;
using Cruddy.Web.Repositories;
using Cruddy.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Cruddy.Web.Controllers
{
	[ModelStateActionFilter]
	public partial class EmployeeController : Controller
	{
		private readonly IEmployeeRepository _employeeRepo;
		private readonly IDepartmentRepository _departmentRepo;

		public EmployeeController(IEmployeeRepository employeeRepo, IDepartmentRepository departmentRepo)
		{
			_employeeRepo = employeeRepo;
			_departmentRepo = departmentRepo;
		}

		public async Task<IActionResult> Index(string? filteredDepartment, string? searchString)
		{
			var employees = _employeeRepo.GetFilterBySearch(filteredDepartment, searchString).ToList();
			var departmentNames = _departmentRepo.GetAll().Select(d => d.Name);

			var employeeDepartmentVM = new EmployeeViewModel()
			{
				Departments = new SelectList(departmentNames.ToList()),
				Employees = employees.ToList()
			};

			return View(employeeDepartmentVM);
		}

		// GET-Create
		public IActionResult Create()
		{
			var employeeDepartmentVM = new EmployeeViewModel
			{
				Departments = new SelectList(_departmentRepo.GetAll(), "Id", "Name")
			};
			return View(employeeDepartmentVM);
		}

		// POST-Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Employee employee)
		{
			_employeeRepo.Insert(employee);
			_employeeRepo.Save();
			return RedirectToAction("Index");
		}

		public IActionResult Update(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var employee = _employeeRepo.GetById(id);

			if (employee == null)
			{
				return NotFound();
			}

			var employeeDepartmentVM = new EmployeeViewModel
			{
				Departments = new SelectList(_departmentRepo.GetAll(), "Id", "Name"),
				Employee = employee
			};

			return View(employeeDepartmentVM);
		}

		// POST-Update
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(Employee employee)
		{
			_employeeRepo.Update(employee);
			_employeeRepo.Save();
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var employee = _employeeRepo.GetById(id);
			if (employee == null)
			{
				return NotFound();
			}

			var employeeDepartmentVM = new EmployeeViewModel()
			{
				Departments = new SelectList(_departmentRepo.GetAll(), "Id", "Name"),
				Employee = employee
			};

			return View(employeeDepartmentVM);
		}

		// POST-Delete
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePost(Employee employee)
		{
			_employeeRepo.Delete(employee);
			_employeeRepo.Save();
			return RedirectToAction("Index");
		}
	}
}