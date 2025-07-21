using System.Linq.Expressions;

namespace GenericDataAccess.Interfaces;

/// <summary>
/// Generic repository interface providing common data access operations.
/// </summary>
/// <typeparam name="T">The entity type that implements IEntity.</typeparam>
public interface IGenericRepository<T> where T : class, IEntity
{
    /// <summary>
    /// Gets an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Gets an entity by its integer identifier.
    /// </summary>
    /// <param name="id">The integer identifier of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Gets all entities of the specified type.
    /// </summary>
    /// <returns>A collection of all entities.</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Inserts a new entity into the database.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    /// <returns>The inserted entity with generated ID.</returns>
    Task<T> PostAsync(T entity);

    /// <summary>
    /// Filters entities based on a WHERE clause and parameters.
    /// </summary>
    /// <param name="whereClause">The WHERE clause for filtering (e.g., "Name = @Name AND Age > @MinAge").</param>
    /// <param name="parameters">The parameters for the WHERE clause.</param>
    /// <returns>A collection of filtered entities.</returns>
    Task<IEnumerable<T>> FilterAsync(string whereClause, object parameters);

    /// <summary>
    /// Performs cursor-based pagination using the specified cursor field.
    /// </summary>
    /// <param name="limit">The maximum number of records to return.</param>
    /// <param name="lastCursorValue">The cursor value from the last record of the previous page.</param>
    /// <param name="cursorField">The field to use as cursor (defaults to Id).</param>
    /// <returns>A collection of entities for the current page.</returns>
    Task<IEnumerable<T>> CursorPaginationAsync(int limit, string? lastCursorValue = null, string cursorField = "Id");

    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>The updated entity.</returns>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Deletes an entity from the database.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <returns>true if the entity was deleted; otherwise, false.</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Counts the total number of entities.
    /// </summary>
    /// <returns>The total count of entities.</returns>
    Task<int> CountAsync();

    /// <summary>
    /// Checks if an entity with the specified ID exists.
    /// </summary>
    /// <param name="id">The unique identifier to check.</param>
    /// <returns>true the entity exists; otherwise, false.</returns>
    Task<bool> ExistsAsync(Guid id);
}