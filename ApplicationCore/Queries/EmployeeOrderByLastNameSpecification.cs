using ApplicationCore.Models;

namespace ApplicationCore.Queries;

public class EmployeeOrderByLastNameSpecification : BaseSpecification<Employee>
{
	public EmployeeOrderByLastNameSpecification()
	{
		OrderBy = ThenBy = e => e.LastName.ToLower();
	}
}