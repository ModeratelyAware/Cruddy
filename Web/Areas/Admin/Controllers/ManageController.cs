using Cruddy.Data;
using Cruddy.Application.Models;
using Cruddy.Web.Filters;
using Cruddy.Web.Repositories;
using Cruddy.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	[LoginRedirectedAuthorize]
	[ModelStateActionFilter]
	public class ManageController : Controller
	{
		private readonly IEmployeeRepository _employeeRepo;
		private readonly IDepartmentRepository _departmentRepo;

		public ManageController(IEmployeeRepository employeeRepo, IDepartmentRepository departmentRepo)
		{
			_employeeRepo = employeeRepo;
			_departmentRepo = departmentRepo;
		}

		public async Task<IActionResult> Index(string? filteredDepartment, string? searchString)
		{
			var employees = await _employeeRepo.GetAllFiltered(filteredDepartment, searchString);
			var departmentNames = await _departmentRepo.GetAllByName();

			var employeeDepartmentVM = new EmployeeViewModel()
			{
				Employees = employees.ToList(),
				Departments = new SelectList(departmentNames)
			};

			return View(employeeDepartmentVM);
		}

		public async Task<IActionResult> Create()
		{
			var departmentQuery = await _departmentRepo.GetAll();
			var departmentList = await departmentQuery.ToListAsync();

			var employeeDepartmentVM = new EmployeeViewModel
			{
				Departments = new SelectList(departmentList, "Id", "Name")
			};

			return View(employeeDepartmentVM);
		}

		public async Task<IActionResult> Update(int? id)
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

			var departmentQuery = await _departmentRepo.GetAll();
			var departmentList = await departmentQuery.ToListAsync();

			var employeeDepartmentVM = new EmployeeViewModel
			{
				Departments = new SelectList(departmentList, "Id", "Name"),
				Employee = employee
			};

			return View(employeeDepartmentVM);
		}

		public async Task<IActionResult> Delete(int? id)
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

			var departmentQuery = await _departmentRepo.GetAll();
			var departmentList = await departmentQuery.ToListAsync();

			var employeeDepartmentVM = new EmployeeViewModel()
			{
				Departments = new SelectList(departmentList, "Id", "Name"),
				Employee = employee
			};

			return View(employeeDepartmentVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Employee employee)
		{
			_employeeRepo.Insert(employee);
			_employeeRepo.Save();
			return RedirectToAction("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(Employee employee)
		{
			_employeeRepo.Update(employee);
			_employeeRepo.Save();
			return RedirectToAction("Index");
		}

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