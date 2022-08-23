using ApplicationCore.Models;

namespace ApplicationCore.Queries;

public class EmployeeDepartmentSpecification : BaseSpecification<Employee>
{
	public EmployeeDepartmentSpecification(string? filteredDepartment)
	{
		if (!string.IsNullOrEmpty(filteredDepartment))
		{
			filteredDepartment = filteredDepartment.ToLower();
			Criteria = e => e.Department.Name.ToLower().Contains(filteredDepartment);
		}
		Includes.Add(e => e.Department);
	}
}