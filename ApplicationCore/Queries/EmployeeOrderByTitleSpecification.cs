using ApplicationCore.Models;

namespace ApplicationCore.Queries;

public class EmployeeOrderByTitleSpecification : BaseSpecification<Employee>
{
	public EmployeeOrderByTitleSpecification()
	{
		OrderBy = e => e.Title.ToLower() == "vp" ? 0
					   : e.Title.ToLower() == "director" ? 1
					   : e.Title.ToLower() == "assistant director" ? 2
					   : e.Title.ToLower() == "manager" ? 3
					   : e.Title.ToLower() == "assistant manager" ? 4
					   : e.Title.ToLower() == "supervisor" ? 5
					   : int.MaxValue;

		ThenBy = e => e.FirstName;
	}
}