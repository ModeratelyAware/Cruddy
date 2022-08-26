using ApplicationCore.Models;
using ApplicationCore.Queries;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Attributes;
using Web.ViewModels;

namespace Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[ModelStateValidation]
public class ManageController : Controller
{
	private readonly CruddyDbContext _dbContext;

	public ManageController(CruddyDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<IActionResult> EmployeesSorted(string? filteredDepartment, string? searchString)
	{
		var employees = await _dbContext.Employees.Specify(new EmployeeDepartmentSpecification(filteredDepartment))
												  .Specify(new EmployeeSearchSpecification(searchString))
												  .SpecifyOrderBy(new EmployeeOrderByTitleSpecification())
												  .SpecifyThenBy(new EmployeeOrderByLastNameSpecification())
												  .ToListAsync();

		var departmentList = new SelectList(await _dbContext.Departments.Select(d => d.Name)
																		.ToListAsync());

		var employeeViewModel = new EmployeeViewModel()
		{
			Employees = employees,
			Departments = departmentList
		};

		return View(employeeViewModel);
	}

	public async Task<IActionResult> CreateEmployee()
	{
		var employeeViewModel = new EmployeeViewModel
		{
			Departments = new SelectList(await _dbContext.Departments.ToListAsync(), "Id", "Name")
		};

		return View(employeeViewModel);
	}

	public async Task<IActionResult> UpdateEmployee(int? id)
	{
		var employee = await _dbContext.Employees.Specify(new EmployeeIdSpecification(id))
												 .FirstOrDefaultAsync();

		if (employee == null)
		{
			throw new HttpRequestException("Employee not found.");
		}

		var employeeDepartmentVM = new EmployeeViewModel
		{
			Employee = employee,
			Departments = new SelectList(
				await _dbContext.Departments.ToListAsync(), "Id", "Name")
		};

		return View(employeeDepartmentVM);
	}

	public async Task<IActionResult> DeleteEmployee(int? id)
	{
		var employee = await _dbContext.Employees.Specify(new EmployeeIdSpecification(id))
												 .FirstOrDefaultAsync();

		if (employee == null)
		{
			throw new HttpRequestException("Employee not found.");
		}

		var employeeDepartmentVM = new EmployeeViewModel()
		{
			Employee = employee,
			Departments = new SelectList(
				await _dbContext.Departments.ToListAsync(), "Id", "Name")
		};

		return View(employeeDepartmentVM);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> CreateEmployee(Employee employee)
	{
		await _dbContext.Employees.AddAsync(employee);
		await _dbContext.SaveChangesAsync();
		return RedirectToAction("EmployeesSorted");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> UpdateEmployee(Employee employee)
	{
		_dbContext.Employees.Update(employee);
		await _dbContext.SaveChangesAsync();
		return RedirectToAction("EmployeesSorted");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteEmployee(Employee employee)
	{
		_dbContext.Employees.Remove(employee);
		await _dbContext.SaveChangesAsync();
		return RedirectToAction("EmployeesSorted");
	}
}