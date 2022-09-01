using ApplicationCore.Queries;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels;

namespace Web.Areas.Employees.Controllers;

[Area("Employees")]
[Route("[area]")]
public class DisplayController : Controller
{
	private readonly CruddyDbContext _dbContext;

	public DisplayController(CruddyDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	[HttpGet("")]
	public async Task<IActionResult> EmployeesSorted(string? filteredDepartment, string? searchString)
	{
		var employees = await _dbContext.Employees.Specify(new EmployeeDepartmentSpecification(filteredDepartment))
												  .Specify(new EmployeeSearchSpecification(searchString))
												  .SpecifyOrderBy(new EmployeeOrderByTitleSpecification())
												  .SpecifyThenBy(new EmployeeOrderByLastNameSpecification())
												  .ToListAsync();

		var departmentList = new SelectList(await _dbContext.Departments.Select(d => d.Name).ToListAsync());

		var model = new EmployeeViewModel()
		{
			Employees = employees,
			Departments = departmentList
		};

		return View(model);
	}
}