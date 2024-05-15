namespace BuildingBlocks.Domain.SpecificationConfig;

public static class SpecificationExtensions
{
    public static IQueryable<TEntity> Specify<TEntity>(this IQueryable<TEntity> query, Specification<TEntity> specification)
    {
        var queryable = query;

        /*if (specification.IncludeExpressions.Count != 0)
        {
            queryable = specification.IncludeExpressions.Aggregate(queryable,
                (current, includeExpression) => current.Include(includeExpression));
        }

        if (specification.IncludeStrings.Count != 0)
        {
            queryable = specification.IncludeStrings.Aggregate(queryable,
                (current, IncludeStrings) => current.Include(IncludeStrings));
        }*/

        if (specification.Criteria is not null)
        {
            queryable = queryable.Where(specification.Criteria);
        }

        if (specification.OrderByExpression is not null)
        {
            var orderedQuery = queryable.OrderBy(specification.OrderByExpression);
            if (specification.ThenOrderByExpression is not null) queryable = orderedQuery.ThenBy(specification.ThenOrderByExpression);
            else if (specification.ThenOrderByDescendingExpression is not null) queryable = orderedQuery.ThenByDescending(specification.ThenOrderByDescendingExpression);
            else queryable = orderedQuery;
        }

        if (specification.OrderByDescendingExpression is not null)
        {
            var orderedQuery = queryable.OrderBy(specification.OrderByDescendingExpression);
            if (specification.ThenOrderByExpression is not null) queryable = orderedQuery.ThenBy(specification.ThenOrderByExpression);
            else if (specification.ThenOrderByDescendingExpression is not null) queryable = orderedQuery.ThenByDescending(specification.ThenOrderByDescendingExpression);
            else queryable = orderedQuery;
        }

        if (specification.OrderByDescendingExpression is not null)
        {
            queryable = queryable.OrderByDescending(specification.OrderByDescendingExpression);
        }

        /*
        if (specification.IsPagingEnabled)
        {
            queryable = queryable.Skip(specification.Skip).Take(specification.Take);
        }
        */

        return queryable;
    }
}