using ApplicationCore.Models;

namespace ApplicationCore.Queries;

public class EmployeeIdSpecification : BaseSpecification<Employee>
{
	public EmployeeIdSpecification(int? id)
	{
		if (id != null)
		{
			Criteria = e => e.Id == id;
		}
	}
}