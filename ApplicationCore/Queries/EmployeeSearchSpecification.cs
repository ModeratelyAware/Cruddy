using ApplicationCore.Models;

namespace ApplicationCore.Queries;

public class EmployeeSearchSpecification : BaseSpecification<Employee>
{
	public EmployeeSearchSpecification(string? searchString)
	{
		if (!string.IsNullOrEmpty(searchString))
		{
			searchString = searchString.ToLower();
			Criteria = e => e.FirstName.ToLower().Contains(searchString)
							|| e.LastName.ToLower().Contains(searchString)
							|| e.Ext.ToString().ToLower().Contains(searchString)
							|| e.Phone1.ToLower().Contains(searchString)
							|| e.Phone2.ToLower().Contains(searchString);
		}
	}
}