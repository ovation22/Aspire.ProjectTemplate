using System.Linq.Expressions;
using System.Reflection;
using Ardalis.Specification;
using Aspire.ProjectTemplate.Core.Interfaces.Specifications;
using Aspire.ProjectTemplate.Core.Models;

namespace Aspire.ProjectTemplate.Core.Specifications;

/// <summary>
/// Represents a paginated specification for entities.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="PaginatedSpecification{T}"/> class.
/// </remarks>
/// <param name="pageNumber">The page number.</param>
/// <param name="pageSize">The page size.</param>
public abstract class PaginatedSpecification<T>(int pageNumber, int pageSize) : Specification<T>, IPaginatedSpecification<T>
{
    /// <inheritdoc />
    public int PageNumber { get; } = pageNumber;

    /// <inheritdoc />
    public int PageSize { get; } = pageSize;

    /// <summary>
    /// Checks if the specified property exists in the entity type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="propertyName">The name of the property to check.</param>
    /// <returns>True if the property exists; otherwise, false.</returns>
    protected static bool IsEntityProperty(string propertyName)
    {
        return GetPropertyExpression(propertyName) != null;
    }

    /// <summary>
    /// Gets the property expression for the specified property.
    /// </summary>
    /// <param name="propertyName">The name of the property.</param>
    /// <returns>The property expression if found; otherwise null.</returns>
    private static PropertyInfo? GetPropertyExpression(string propertyName)
    {
        var properties = propertyName.Split('.');
        var type = typeof(T);
        PropertyInfo? property = null;

        foreach(var prop in properties)
        {
            property = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if(property == null)
            {
                return null;
            }

            type = property.PropertyType;
        }

        return property;
    }

    /// <summary>
    /// Builds the filter expression based on the filter operator and value.
    /// </summary>
    /// <param name="filterBy">The property name to filter by.</param>
    /// <param name="filter">The filter object containing the operator and value.</param>
    /// <param name="propertyMappings">The custom property mappings dictionary.</param>
    /// <returns>An expression representing the filter condition.</returns>
    protected virtual Expression<Func<T, bool>> BuildFilterExpression(string filterBy, Filter filter, Dictionary<string, string> propertyMappings)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        // Map the filterBy to the actual property path if it exists in the dictionary
        if(propertyMappings.TryGetValue(filterBy, out var actualPropertyPath))
        {
            filterBy = actualPropertyPath;
        }

        Expression property = parameter;

        // Split the filterBy string into parts to handle nested properties
        foreach(var member in filterBy.Split('.'))
        {
            property = Expression.Property(property, member);
        }

        Expression body;

        if(filter.Operator != FilterOperator.Between)
        {
            if(filter.Value == null)
            {
                throw new InvalidOperationException("Value is required for the selected operator.");
            }

            Expression constant = property.Type switch
            {
                { } t when t == typeof(int) => Expression.Constant(int.Parse(filter.Value)),
                { } t when t == typeof(long) => Expression.Constant(long.Parse(filter.Value)),
                { } t when t == typeof(double) => Expression.Constant(double.Parse(filter.Value)),
                { } t when t == typeof(DateTime) => Expression.Constant(DateTime.Parse(filter.Value)),
                { } t when t == typeof(bool) => Expression.Constant(bool.Parse(filter.Value)),
                _ => Expression.Constant(filter.Value)
            };

            body = filter.Operator switch
            {
                FilterOperator.Eq => Expression.Equal(property, constant),
                FilterOperator.Ne => Expression.NotEqual(property, constant),
                FilterOperator.Contains => Expression.Call(property, "Contains", null, constant),
                FilterOperator.StartsWith => Expression.Call(property, "StartsWith", null, constant),
                FilterOperator.EndsWith => Expression.Call(property, "EndsWith", null, constant),
                FilterOperator.Gt => Expression.GreaterThan(property, constant),
                FilterOperator.Gte => Expression.GreaterThanOrEqual(property, constant),
                FilterOperator.Lt => Expression.LessThan(property, constant),
                FilterOperator.Lte => Expression.LessThanOrEqual(property, constant),
                _ => throw new NotSupportedException($"Filter operator '{filter.Operator}' is not supported.")
            };
        }
        else
        {
            if(filter.ValueFrom == null || filter.ValueTo == null)
            {
                throw new InvalidOperationException("ValueFrom and ValueTo are required for the selected operator.");
            }

            body = filter.Operator switch
            {
                FilterOperator.Between when property.Type == typeof(int) =>
                    Expression.AndAlso(
                        Expression.GreaterThanOrEqual(property, Expression.Constant(int.Parse(filter.ValueFrom))),
                        Expression.LessThanOrEqual(property, Expression.Constant(int.Parse(filter.ValueTo)))),
                FilterOperator.Between when property.Type == typeof(DateTime) =>
                    Expression.AndAlso(
                        Expression.GreaterThanOrEqual(property, Expression.Constant(DateTime.Parse(filter.ValueFrom))),
                        Expression.LessThanOrEqual(property, Expression.Constant(DateTime.Parse(filter.ValueTo)))),
                _ => throw new NotSupportedException($"Filter operator '{filter.Operator}' is not supported.")
            };
        }

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// Combines multiple filter expressions with an OR operator.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <param name="filterExpressions">The filter expressions to combine.</param>
    /// <returns>The combined filter expression with an OR operator.</returns>
    protected Expression<Func<T, bool>> CombineFilterExpressionsWithOr(IEnumerable<Expression<Func<T, bool>>> filterExpressions)
    {
        // Create a parameter expression representing the entity
        var parameter = Expression.Parameter(typeof(T), "x");

        // Create initial expression with "false" to start the OR chain
        Expression combinedFilterExpression = Expression.Constant(false);

        // Combine each filter expression with OR operator
        foreach(var filterExpression in filterExpressions)
        {
            // Invoke the filter expression with the entity parameter
            var invokedFilterExpression = Expression.Invoke(filterExpression, parameter);

            // Combine the current filter expression with the existing combined expression using OR operator
            combinedFilterExpression = Expression.OrElse(combinedFilterExpression, invokedFilterExpression);
        }

        // Create a lambda expression representing the combined filter expression
        return Expression.Lambda<Func<T, bool>>(combinedFilterExpression, parameter);
    }
    
    /// <summary>
    /// Applies sorting logic based on entity properties.
    /// </summary>
    /// <param name="sortBy">The property to sort by.</param>
    /// <param name="sortDirection">The direction of the sort. If "DESC", sorting is in descending order; otherwise, sorting is in ascending order.</param>
    /// <remarks>
    /// This method creates an order by expression for the specified property and applies it to the query. 
    /// If the sort direction is "DESC", the query is ordered in descending order; otherwise, it is ordered in ascending order.
    /// </remarks>
    protected void ApplySorting(string sortBy, SortDirection sortDirection)
    {
        ApplySorting(sortBy, sortDirection, new Dictionary<string, string>());
    }

    /// <summary>
    /// Applies sorting logic based on entity properties.
    /// </summary>
    /// <param name="sortBy">The property to sort by.</param>
    /// <param name="sortDirection">The direction of the sort. If "DESC", sorting is in descending order; otherwise, sorting is in ascending order.</param>
    /// <param name="propertyMappings">The custom property mappings dictionary.</param>
    /// <remarks>
    /// This method creates an order by expression for the specified property and applies it to the query. 
    /// If the sort direction is "DESC", the query is ordered in descending order; otherwise, it is ordered in ascending order.
    /// </remarks>
    protected void ApplySorting(string sortBy, SortDirection sortDirection, Dictionary<string, string> propertyMappings)
    {
        if(propertyMappings.TryGetValue(sortBy, out var mappedProperty))
        {
            sortBy = mappedProperty;
        }

        if(IsEntityProperty(sortBy))
        {
            if(sortDirection == SortDirection.Desc)
            {
                Query.OrderByDescending(CreateOrderByExpression(sortBy)!);
            }
            else
            {
                Query.OrderBy(CreateOrderByExpression(sortBy)!);
            }
        }
        else
        {
            throw new NotSupportedException($"Order By property '{sortBy}' is not supported.");
        }
    }

    /// <summary>
    /// Creates an order by expression for the specified property in the entity type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="propertyPath">The path of the property to order by, supporting dot notation for nested properties.</param>
    /// <returns>An expression representing the order by clause.</returns>
    protected static Expression<Func<T, object>> CreateOrderByExpression(string propertyPath)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        // Split the propertyPath into parts to handle nested properties
        Expression property = parameter;

        foreach(var member in propertyPath.Split('.'))
        {
            property = Expression.Property(parameter, member);
        }

        var propertyAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(propertyAsObject, parameter);
    }

    /// <summary>
    /// Applies filters with an OR logic.
    /// </summary>
    /// <param name="filters">The dictionary of filters to apply.</param>
    protected void ApplyOrFilters(Dictionary<string, Filter> filters)
    {
        ApplyOrFilters(filters, new Dictionary<string, string>());
    }

    /// <summary>
    /// Applies filters with an OR logic.
    /// </summary>
    /// <param name="filters">The dictionary of filters to apply.</param>
    /// <param name="propertyMappings">The custom property mappings dictionary.</param>
    protected void ApplyOrFilters(Dictionary<string, Filter> filters, Dictionary<string, string> propertyMappings)
    {
        var filterList = filters.ToList();
        var filterExpressions = filterList.Select(filter =>
            BuildFilterExpression(filter.Key, filter.Value, propertyMappings));

        var combinedFilterExpression = CombineFilterExpressionsWithOr(filterExpressions);
        Query.Where(combinedFilterExpression);
    }

    /// <summary>
    /// Applies filters with an AND logic.
    /// </summary>
    /// <param name="filters">The dictionary of filters to apply.</param>
    protected void ApplyAndFilters(Dictionary<string, Filter> filters)
    {
        ApplyAndFilters(filters, new Dictionary<string, string>());
    }

    /// <summary>
    /// Applies filters with an AND logic.
    /// </summary>
    /// <param name="filters">The dictionary of filters to apply.</param>
    /// <param name="propertyMappings">The custom property mappings dictionary.</param>
    protected void ApplyAndFilters(Dictionary<string, Filter> filters, Dictionary<string, string> propertyMappings)
    {
        foreach(var filterExpression in filters.Select(filter =>
                    BuildFilterExpression(filter.Key, filter.Value, propertyMappings)))
        {
            Query.Where(filterExpression);
        }
    }
}
