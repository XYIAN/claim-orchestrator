namespace GenericDataAccess.Interfaces;

/// <summary>
/// Base interface for all entities that have a unique identifier.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    Guid Id { get; set; }
}