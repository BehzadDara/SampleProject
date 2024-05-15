using BuildingBlocks.Domain.Enums;
using System.Linq.Expressions;

namespace BuildingBlocks.Domain.SpecificationConfig;

public abstract class Specification<TEntity>
{
    protected Specification()
    {
    }

    protected Specification(Expression<Func<TEntity, bool>> criteria) => Criteria = criteria;

    public Expression<Func<TEntity, bool>>? Criteria { get; set; }

    public List<string> IncludeStrings { get; set; } = [];
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }

    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    public Expression<Func<TEntity, object>>? ThenOrderByExpression { get; private set; }

    public Expression<Func<TEntity, object>>? ThenOrderByDescendingExpression { get; private set; }

    protected void AddInclude(Expression<Func<TEntity, object>> table) => IncludeExpressions.Add(table);

    protected void AddInclude(string table) => IncludeStrings.Add(table);
    protected void AddOrderBy(Expression<Func<TEntity, object>> orderExpression, OrderType orderType)
    {
        if (orderType == OrderType.Descending)
        {
            OrderByDescendingExpression = orderExpression;
        }
        else
        {
            OrderByExpression = orderExpression;
        }
    }

    protected void AddThenOrderBy(Expression<Func<TEntity, object>> orderExpression, OrderType orderType)
    {
        if (orderType == OrderType.Descending)
        {
            ThenOrderByDescendingExpression = orderExpression;
        }
        else
        {
            ThenOrderByExpression = orderExpression;
        }
    }
    protected void AddPaging(int pageSize, int pageNumber)
    {
        Skip = (pageNumber - 1) * pageSize;
        Take = pageSize;
        IsPagingEnabled = true;
    }

    protected void AddCriteria(Expression<Func<TEntity, bool>> predict)
    {
        if (Criteria is null)
        {
            Criteria = predict;
        }
        else
        {
            var left = Criteria.Parameters[0];
            var visitor = new ReplaceParameterVisitor(predict.Parameters[0], left);
            var right = visitor.Visit(predict.Body);
            Criteria = Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(Criteria.Body, right), left);
        }
    }

    private class ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter) : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (ReferenceEquals(node, oldParameter))
            {
                return newParameter;
            }

            return base.VisitParameter(node);
        }
    }
}
