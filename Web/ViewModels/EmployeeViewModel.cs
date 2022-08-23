using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewModels;

public class EmployeeViewModel
{
	public Employee Employee { get; set; }
	public IEnumerable<Employee> Employees { get; set; }
	public SelectList Departments { get; set; }
	public string FilteredDepartment { get; set; }
	public string SearchString { get; set; }
}