using System.Linq.Expressions;

namespace ApplicationCore.Queries;

public interface ISpecification<T>
{
	Expression<Func<T, bool>>? Criteria { get; }
	Expression<Func<T, object>>? OrderBy { get; }
	Expression<Func<T, object>>? ThenBy { get; }
	List<Expression<Func<T, object>>> Includes { get; }
	List<string> IncludeStrings { get; }
}