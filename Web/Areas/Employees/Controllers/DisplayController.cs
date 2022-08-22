using Cruddy.Data;
using Cruddy.Application.Models;
using Cruddy.Web.Filters;
using Cruddy.Web.Repositories;
using Cruddy.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Cruddy.Web.Areas.Employees.Controllers
{
	[Area("Employees")]
	[ModelStateActionFilter]
	public class DisplayController : Controller
	{
		private readonly IEmployeeRepository _employeeRepo;
		private readonly IDepartmentRepository _departmentRepo;

		public DisplayController(IEmployeeRepository employeeRepo, IDepartmentRepository departmentRepo)
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
	}
}