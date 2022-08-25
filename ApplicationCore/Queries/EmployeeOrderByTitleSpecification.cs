using ApplicationCore.Models;

namespace ApplicationCore.Queries;

public class EmployeeOrderByTitleSpecification : BaseSpecification<Employee>
{
	//Not very efficient, could likely improve this in future.
	public EmployeeOrderByTitleSpecification()
	{
		OrderBy = e => e.Title.ToLower().Contains("vp") ? 0
					   : e.Title.ToLower().Contains("director") && !e.Title.ToLower().Contains("assistant") ? 1
					   : e.Title.ToLower().Contains("director") && e.Title.ToLower().Contains("assistant") ? 2
					   : e.Title.ToLower().Contains("manager") && !e.Title.ToLower().Contains("assistant") ? 3
					   : e.Title.ToLower().Contains("manager") && e.Title.ToLower().Contains("assistant") ? 4
					   : e.Title.ToLower().Contains("supervisor") ? 5
					   : int.MaxValue;

		ThenBy = e => e.FirstName;
	}
}