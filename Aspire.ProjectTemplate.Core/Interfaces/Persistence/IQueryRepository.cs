using System.Linq.Expressions;
using Ardalis.Specification;
using Aspire.ProjectTemplate.Core.Interfaces.Specifications;
using Aspire.ProjectTemplate.Core.Models;

namespace Aspire.ProjectTemplate.Core.Interfaces.Persistence;

/// <summary>
/// Interface defining query repository operations.
/// </summary>
public interface IQueryRepository
{
    /// <summary>
    /// Gets a single entity type <typeparamref name="T"/> by its identifier.
    /// </summary>
    /// <typeparam name="T">The type of the entity to retrieve.</typeparam>
    /// <param name="id">The identifier of the entity to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains the entity of type <typeparamref name="T"/> if found; otherwise, null.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null.</exception>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>

    Task<T?> FindAsync<T>(object id, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    /// Gets a single entity matching the given expression.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="expression">The expression to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<T?> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    /// Gets a single entity matching the given specification.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="specification">The specification to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<T?> SingleOrDefaultAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Gets a single entity matching the given expression and projects it to a result type.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TProjection">The type of the result.</typeparam>
    /// <param name="expression">The expression to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the operation is invalid due to a missing dependency.</exception>
    Task<TProjection?> SingleOrDefaultAsync<T, TProjection>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Gets a single entity matching the given specification and projects it to a result type.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TProjection">The type of the result.</typeparam>
    /// <param name="specification">The specification to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the operation is invalid due to a missing dependency.</exception>
    Task<TProjection?> SingleOrDefaultAsync<T, TProjection>(ISpecification<T> specification, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Gets the first entity matching the given expression.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="expression">The expression to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    /// Gets the first entity matching the given specification.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="specification">The specification to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<T?> FirstOrDefaultAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Gets the first entity matching the given expression and projects it to a result type.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TProjection">The type of the result.</typeparam>
    /// <param name="expression">The expression to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the operation is invalid due to a missing dependency.</exception>
    Task<TProjection?> FirstOrDefaultAsync<T, TProjection>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Gets the first entity matching the given specification and projects it to a result type.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TProjection">The type of the result.</typeparam>
    /// <param name="specification">The specification to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the operation is invalid due to a missing dependency.</exception>
    Task<TProjection?> FirstOrDefaultAsync<T, TProjection>(ISpecification<T> specification, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Gets all entities of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An enumerable of entities.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<IEnumerable<T>> GetAllAsync<T>(CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Lists entities matching the given expression.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="expression">The expression to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of entities.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<List<T>> ListAsync<T>(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Lists entities matching the given specification.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="specification">The specification to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of entity results.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<List<T>> ListAsync<T>(ISpecification<T> specification,
        CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Lists entities matching the given specification and projects them to a result type.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TProjection">The type of the result.</typeparam>
    /// <param name="expression">The expression to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of projected results.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the operation is invalid due to a missing dependency.</exception>
    Task<List<TProjection>> ListAsync<T, TProjection>(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Lists entities matching the given specification and projects them to a result type.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TProjection">The type of the result.</typeparam>
    /// <param name="specification">The specification to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of projected results.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the operation is invalid due to a missing dependency.</exception>
    Task<List<TProjection>> ListAsync<T, TProjection>(ISpecification<T> specification,
        CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Lists entities matching the given specification and projects them to a result type.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="specification">The specification to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of projected results.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<List<TResult>> ListAsync<T, TResult>(ISpecification<T, TResult> specification,
        CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Groups entities matching the given specification and projects them to a result type.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TGrouping">The type of the grouping.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="specification">The specification to match.</param>
    /// <param name="groupBy">The grouping to apply.</param>
    /// <param name="selector">The selector to apply.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of projected results.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<List<TResult>> ListAsync<T, TGrouping, TResult>(ISpecification<T> specification,
        Expression<Func<T, TGrouping>> groupBy,
        Expression<Func<IGrouping<TGrouping, T>, TResult>> selector,
        CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Lists paginated entities matching the given specification.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="specification">The specification to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paginated list of entities.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<PagedList<T>> ListAsync<T>(IPaginatedSpecification<T> specification,
        CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Lists paginated entities matching the given specification and projects them to a result type.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="specification">The specification to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paginated list of projected results.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<PagedList<TResult>> ListAsync<T, TResult>(IPaginatedSpecification<T> specification,
        CancellationToken cancellationToken = default) where T : class;
    
    /// <summary>
    /// Lists paginated entities matching the given specification and projects them to a result type.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="specification">The specification to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paginated list of projected results.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<PagedList<TResult>> ListAsync<T, TResult>(IPaginatedSpecification<T, TResult> specification,
        CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Checks whether any entities exist.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if there were any matching entities, otherwise false.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<bool> AnyAsync<T>(CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Checks whether any entities match the given expression.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="expression">The expression to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if there were any matching entities, otherwise false.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Checks whether any entities match the given specification.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="specification">The specification to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if there were any matching entities, otherwise false.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<bool> AnyAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Counts the number of entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of matching entities.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<int> CountAsync<T>(CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Counts the number of entities matching the given expression.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="expression">The expression to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of matching entities.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<int> CountAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Counts the number of entities matching the given specification.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="specification">The specification to match.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of matching entities.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    Task<int> CountAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken = default) where T : class;
}
