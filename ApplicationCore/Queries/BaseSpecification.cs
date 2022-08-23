using System.Linq.Expressions;

namespace ApplicationCore.Queries;

public abstract class BaseSpecification<T> : ISpecification<T>
{
	public Expression<Func<T, bool>>? Criteria { get; set; }
	public Expression<Func<T, object>>? OrderBy { get; set; }
	public Expression<Func<T, object>>? ThenBy { get; set; }
	public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
	public List<string> IncludeStrings { get; } = new List<string>();

	protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
	{
		Includes.Add(includeExpression);
	}

	protected virtual void AddInclude(string includeString)
	{
		IncludeStrings.Add(includeString);
	}
}