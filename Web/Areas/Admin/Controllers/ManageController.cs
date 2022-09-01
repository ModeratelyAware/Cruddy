using ApplicationCore.Models;
using ApplicationCore.Queries;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]")]
[Authorize(Roles = "Admin")]
public class ManageController : Controller
{
	private readonly CruddyDbContext _dbContext;

	public ManageController(CruddyDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	[HttpGet("Employees")]
	public async Task<IActionResult> EmployeesSorted(string? filteredDepartment, string? searchString)
	{
		var employees = await _dbContext.Employees.Specify(new EmployeeDepartmentSpecification(filteredDepartment))
												  .Specify(new EmployeeSearchSpecification(searchString))
												  .SpecifyOrderBy(new EmployeeOrderByTitleSpecification())
												  .SpecifyThenBy(new EmployeeOrderByLastNameSpecification())
												  .ToListAsync();

		var departmentList = new SelectList(await _dbContext.Departments.Select(d => d.Name)
																		.ToListAsync());

		var model = new EmployeeViewModel()
		{
			Employees = employees,
			Departments = departmentList
		};

		return View(model);
	}

	[HttpGet("Employees/Create")]
	public async Task<IActionResult> CreateEmployee()
	{
		var model = new EmployeeViewModel
		{
			Departments = new SelectList(await _dbContext.Departments.ToListAsync(), "Id", "Name")
		};

		return View(model);
	}

	[HttpGet("Employees/Update/{id}")]
	public async Task<IActionResult> UpdateEmployee(int? id)
	{
		var employee = await _dbContext.Employees.Specify(new EmployeeIdSpecification(id))
												 .FirstOrDefaultAsync();

		if (employee == null)
		{
			throw new HttpRequestException("Employee not found.");
		}

		var model = new EmployeeViewModel
		{
			Employee = employee,
			Departments = new SelectList(
				await _dbContext.Departments.ToListAsync(), "Id", "Name")
		};

		return View(model);
	}

	[HttpGet("Employees/Delete/{id}")]
	public async Task<IActionResult> DeleteEmployee(int? id)
	{
		var employee = await _dbContext.Employees.Specify(new EmployeeIdSpecification(id))
												 .FirstOrDefaultAsync();

		if (employee == null)
		{
			throw new HttpRequestException("Employee not found.");
		}

		var model = new EmployeeViewModel()
		{
			Employee = employee,
			Departments = new SelectList(
				await _dbContext.Departments.ToListAsync(), "Id", "Name")
		};

		return View(model);
	}

	[HttpPost("Employees/Create")]
	public async Task<IActionResult> CreateEmployee(EmployeeViewModel model)
	{
		if (!ModelState.IsValid)
		{
			ModelState.AddModelError("",
				"Unable to create employee. Modelstate is invalid.");

			return View(model);
		}

		await _dbContext.Employees.AddAsync(model.Employee);
		await _dbContext.SaveChangesAsync();
		return RedirectToAction("EmployeesSorted");
	}

	[HttpPost("Employees/Update/{id}")]
	public async Task<IActionResult> UpdateEmployee(EmployeeViewModel model)
	{
		if (!ModelState.IsValid)
		{
			ModelState.AddModelError("",
				"Unable to save changes. Modelstate is invalid.");

			return View(model);
		}

		_dbContext.Employees.Update(model.Employee);
		await _dbContext.SaveChangesAsync();
		return RedirectToAction("EmployeesSorted");
	}

	[HttpPost("Employees/Delete/{id}")]
	public async Task<IActionResult> DeleteEmployee(EmployeeViewModel model)
	{
		if (!ModelState.IsValid)
		{
			ModelState.AddModelError("",
				"Unable to delete employee. Modelstate is invalid.");

			return View(model);
		}

		_dbContext.Employees.Remove(model.Employee);
		await _dbContext.SaveChangesAsync();
		return RedirectToAction("EmployeesSorted");
	}
}