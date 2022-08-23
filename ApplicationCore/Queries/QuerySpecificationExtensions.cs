using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Queries;

public static class QuerySpecificationExtensions
{
	public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class
	{
		// fetch a Queryable that includes all expression-based includes
		var queryableResultWithIncludes = spec.Includes
			.Aggregate(query,
				(current, include) => current.Include(include));

		// modify the IQueryable to include any string-based include statements
		var secondaryResult = spec.IncludeStrings
			.Aggregate(queryableResultWithIncludes,
				(current, include) => current.Include(include));

		// return the result of the query using the specification's criteria expression
		if (spec.Criteria == null)
		{
			return secondaryResult;
		}

		return secondaryResult.Where(spec.Criteria);
	}

	public static IOrderedQueryable<T> SpecifyOrderBy<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class
	{
		// fetch a Queryable that includes all expression-based includes
		var queryableResultWithIncludes = spec.Includes
			.Aggregate(query,
				(current, include) => current.Include(include));

		// modify the IQueryable to include any string-based include statements
		var secondaryResult = spec.IncludeStrings
			.Aggregate(queryableResultWithIncludes,
				(current, include) => current.Include(include));

		// return the result of the query using the specification's criteria expression

		if (spec.OrderBy == null)
		{
			return secondaryResult.OrderBy(e => 0);
		}
		return secondaryResult.OrderBy(spec.OrderBy);
	}

	public static IOrderedQueryable<T> SpecifyThenBy<T>(this IOrderedQueryable<T> query, ISpecification<T> spec) where T : class
	{
		if (spec.ThenBy == null)
		{
			return query;
		}
		return query.ThenBy(spec.ThenBy);
	}
}