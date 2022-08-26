# Cruddy

Simple CRUD application using ASP.NET Core MVC, SQLite, and EF Core. My goal was to familiarize myself more with web development and form a better understanding of architecture and best practices.

### Architecture

```mermaid
flowchart LR;  
Web --> Infrastructure
Infrastructure --> ApplicationCore;
```

### Specification Pattern
I utilized the specification pattern for queries,  a repository seemed redundant considering the use of EF Core. This allowed me to create building blocks for more complex queries and minimize the code in my controllers.

```csharp
_dbContext.Employees.Specify(new EmployeeDepartmentSpecification(filteredDepartment))
		    .Specify(new EmployeeSearchSpecification(searchString))
		    .SpecifyOrderBy(new EmployeeOrderByTitleSpecification())
		    .SpecifyThenBy(new EmployeeOrderByLastNameSpecification())
		    .ToListAsync();
```
