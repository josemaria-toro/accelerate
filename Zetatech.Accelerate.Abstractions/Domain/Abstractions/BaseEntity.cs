using System;

namespace Zetatech.Accelerate.Domain.Abstractions;

/// <summary>
/// Represents the base class for implementing custom entities.
/// </summary>
public abstract class BaseEntity : IEntity
{
    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the update date.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Creates a shallow copy of the current object instance.
    /// </summary>
    public virtual Object Clone()
    {
        var type = GetType();
        var instance = Activator.CreateInstance(type);
        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(this);
            property.SetValue(instance, value);
        }

        return instance;
    }
}