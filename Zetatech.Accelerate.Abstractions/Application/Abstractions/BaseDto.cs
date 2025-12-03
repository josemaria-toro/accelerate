using System;

namespace Zetatech.Accelerate.Application.Abstractions;

/// <summary>
/// Represents the base class for implementing custom data transfer objects.
/// </summary>
public abstract class BaseDto : IDto
{
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