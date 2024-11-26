namespace Aspire.ProjectTemplate.Core.Interfaces.Persistence;

/// <summary>
/// Interface defining command repository operations.
/// </summary>
public interface ICommandRepository
{
    /// <summary>
    /// Creates a new entity in the repository.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="entity">The entity to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created entity.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    /// <exception cref="Exception">Thrown when a general error occurs while saving changes.</exception>
    Task<T> CreateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Creates multiple new entities in the repository.
    /// </summary>
    /// <typeparam name="T">The type of the entities.</typeparam>
    /// <param name="entities">The entities to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created entities.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    /// <exception cref="Exception">Thrown when a general error occurs while saving changes.</exception>
    Task<List<T>> CreateAsync<T>(List<T> entities, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated entity.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    /// <exception cref="Exception">Thrown when a general error occurs while saving changes.</exception>
    Task<T> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Updates multiple existing entities in the repository.
    /// </summary>
    /// <typeparam name="T">The type of the entities.</typeparam>
    /// <param name="entities">The entities to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated entities.</returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
    /// <exception cref="Exception">Thrown when a general error occurs while saving changes.</exception>
    Task<List<T>> UpdateAsync<T>(List<T> entities, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task DeleteAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Deletes multiple entities from the repository.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <param name="entities">The entities to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task DeleteAsync<T>(List<T> entities, CancellationToken cancellationToken = default) where T : class;
}
