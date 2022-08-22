using Cruddy.Application.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cruddy.Web.ViewModels
{
	public class EmployeeViewModel
	{
		public Employee Employee { get; set; }
		public List<Employee> Employees { get; set; }
		public SelectList Departments { get; set; }
		public string FilteredDepartment { get; set; }
		public string SearchString { get; set; }
	}
}