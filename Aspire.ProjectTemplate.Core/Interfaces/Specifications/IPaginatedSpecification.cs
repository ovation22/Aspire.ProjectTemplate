using Ardalis.Specification;

namespace Aspire.ProjectTemplate.Core.Interfaces.Specifications;

/// <summary>
/// Interface for a paginated specification.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface IPaginatedSpecification<T> : ISpecification<T>
{
    /// <summary>
    /// Gets the page number.
    /// </summary>
    int PageNumber { get; }

    /// <summary>
    /// Gets the page size.
    /// </summary>
    int PageSize { get; }
}

/// <summary>
/// Interface for a paginated specification with a result type.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public interface IPaginatedSpecification<T, TResult> : ISpecification<T, TResult>
{
    /// <summary>
    /// Gets the page number.
    /// </summary>
    int PageNumber { get; }

    /// <summary>
    /// Gets the page size.
    /// </summary>
    int PageSize { get; }
}
